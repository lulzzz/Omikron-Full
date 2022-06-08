using System;
using MassTransit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Omikron.SharedKernel.Infrastructure.Configuration;
using Omikron.SharedKernel.Infrastructure.Services;

namespace Omikron.SharedKernel.Extensions
{
    public static class BudApiExtensions
    {
        public static IServiceCollection AddBudApiClient(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection
                .Configure<BudConfiguration>(config: configuration.GetSection(key: ApiServices.ApiServicesBudConfigurationKey))
                .AddScoped<IBudPayloadLogging, BlobStorageBudPayloadLogging>()
                .AddScoped<IBudApiService, BudApiService>();
        }

        public static IServiceCollection RegisterProviderIconConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<BudProviderIcons>(configuration.GetSection("BudProviderIcons")).AddSingleton(x => x.GetRequiredService<IOptions<BudProviderIcons>>().Value);
            serviceCollection.Configure<BudProviderIcons>(x =>
            {
                if (x?.Providers == null)
                {
                    throw new InvalidOperationException("Missing configuration for BudProviderIcons");
                }
            });

            return serviceCollection;
        }
    }
}