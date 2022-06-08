using System;
using System.Net.Http;
using Omikron.SharedKernel.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Microsoft.AspNetCore.Http;

namespace Omikron.SharedKernel.Extensions
{
    public static class SecurityExtensions
    {
        public static IServiceCollection AddAuthenticationWithJwtBearer(this IServiceCollection services, IConfiguration configuration)
        { 
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration.GetValue<string>("IdentityServer:Authority");
                    options.Audience = configuration.GetValue<string>("IdentityServer:Audience");
                    options.RequireHttpsMetadata = false;
                });

            return services;
        }

        public static IServiceCollection AddHttpTokenService(this IServiceCollection services, IConfiguration configuration)
        {
            var authority = configuration.GetValue<string>("IdentityServer:Authority");
            var serviceApiSecret = configuration.GetValue<string>("IdentityServer:ServiceApiSecret");

            services.AddHttpClient(nameof(ServiceClientTokenService),
                    client =>
                    {
                        client.BaseAddress = new Uri($"{authority}/connect/token");
                    })
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                }));

            services.AddScoped<ITokenService>(provider =>
                new ServiceClientTokenService(provider.GetService<IHttpClientFactory>(), serviceApiSecret, provider.GetService<IHttpContextAccessor>()));

            return services;
        }
    }
}