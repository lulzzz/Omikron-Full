using System;
using System.IO.Compression;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Security;
using Omikron.SharedKernel.Infrastructure.Jobs;
using Omikron.SharedKernel.Infrastructure.Json;
using Omikron.SharedKernel.Infrastructure.Logging.Middlewares;
using Omikron.SharedKernel.Infrastructure.Serialization.IoC;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Tenants.Accessors;
using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.SharedKernel.Extensions
{
    public static class CommonExtensions
    {
        public static IServiceCollection AddCommonFeature(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var origins = configuration.GetSection(key: "Cors:Origins").Get<string>();
            if (string.IsNullOrWhiteSpace(value: origins))
            {
                throw new Exception(message: "The configuration value 'Cors:Origins' cannot be null or empty.");
            }

            var urls = origins.Split(separator: ";");

            serviceCollection
                .AddHealthChecks();

            serviceCollection.AddJsonSerialization()
                .UseMicrosoftSystemTextJsonProvider(options: DefaultJsonSerializerOptions.DefaultSerializerOptions);

            serviceCollection
                .AddHttpContextAccessor()
                .AddCors(setupAction: o => o.AddPolicy(name: "DefaultPolicy", configurePolicy: builder =>
                {
                    builder
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithOrigins(origins: urls)
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }))
                .AddHostedService<QueuedHostedService>()
                .AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

            serviceCollection
                .AddTransient<ITenantAccessor>(implementationFactory: provider => new TenantAccessorByStaticStrategy(tenantInfo: Tenant.SystemTenant));

            serviceCollection
                .Configure<GzipCompressionProviderOptions>(configureOptions: options => options.Level = CompressionLevel.Fastest)
                .AddResponseCompression(configureOptions: options => options.Providers.Add<GzipCompressionProvider>());

            return serviceCollection;
        }

        public static IMvcBuilder AddJson(this IMvcBuilder builder, params JsonConverter[] converters)
        {
            foreach (var jsonConverter in converters)
            {
                DefaultJsonSerializerOptions.DefaultSerializerOptions.Converters.Add(item: jsonConverter);
            }

            return builder.AddJsonOptions(configure: options =>
            {
                DefaultJsonSerializerOptions.DefaultSerializerOptions.Converters.ForEach(action: c => options.JsonSerializerOptions.Converters.Add(item: c));
                options.JsonSerializerOptions.DefaultIgnoreCondition = DefaultJsonSerializerOptions.DefaultSerializerOptions.DefaultIgnoreCondition;
            });
        }

        public static IApplicationBuilder UseCommonFeature(this IApplicationBuilder app)
        {
            return app
                .UseCors(policyName: "DefaultPolicy")
                .UseMiddleware<CorrelationIdMiddleware>()
                .UseMiddleware<HttpContextSecurityHeadersMiddleware>()
                .UseHealthChecks(path: "/health")
                .UseResponseCompression();
        }
    }
}