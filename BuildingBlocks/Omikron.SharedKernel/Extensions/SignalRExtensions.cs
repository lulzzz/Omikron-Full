using System;
using Omikron.SharedKernel.Infrastructure.Json;
using Omikron.SharedKernel.Infrastructure.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Extensions
{
    public static class SignalRExtensions
    {
        public static IServiceCollection AddSignalR(this IServiceCollection service, IConfiguration configuration)
        {
            var signalRConfiguration = configuration.GetSection("SignalRConfiguration").Get<SignalRConfiguration>();

            if (signalRConfiguration == null)
            {
                throw new ArgumentNullException(nameof(signalRConfiguration));
            }

            var signalRBuilder = service
                .AddSignalR(options => options.EnableDetailedErrors = true)
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions = DefaultJsonSerializerOptions.DefaultSerializerOptions;
                });

            if (signalRConfiguration.Platform == SupportPlatform.AzureSignalR)
            {
                if (string.IsNullOrWhiteSpace(signalRConfiguration.ConnectionString))
                {
                    throw new ArgumentNullException(nameof(signalRConfiguration.ConnectionString));
                }

                signalRBuilder.AddAzureSignalR(signalRConfiguration.ConnectionString);
            }

            return signalRBuilder.Services;
        }

        public static IApplicationBuilder UseSignalRHub<THub>(this IApplicationBuilder app, string pattern) where THub : Hub
        {
            return app.UseEndpoints(endpoints => endpoints.MapHub<THub>(pattern));
        }
    }
}