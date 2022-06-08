using System;
using System.Net.Http.Headers;
using Omikron.SharedKernel.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Omikron.SharedKernel.Extensions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpIdentityServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var uri = configuration.GetValue<string>("Endpoint:IdentityService:Uri");
            services.AddHttpClient<IHttpIdentityService, HttpIdentityService>(
                    client =>
                    {
                        client.BaseAddress = new Uri(uri);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    })
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                }));
            return services;
        }

        public static IServiceCollection AddHttpVaultServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var uri = configuration.GetValue<string>("Endpoint:VaultService:Uri");
            services.AddHttpClient<IHttpVaultService, HttpVaultService>(
                    client =>
                    {
                        client.BaseAddress = new Uri(uri);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    })
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                }));
            return services;
        }
    }
}