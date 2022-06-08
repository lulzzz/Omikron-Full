using System;
using Omikron.SharedKernel.Infrastructure.Asset;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Extensions
{
    public static class AssetManagerExtensions
    {
        public static IServiceCollection AddAzureAssetManager(this IServiceCollection serviceCollection,
            Action<AzureAssetManagerConfiguration> configure)
        {
            var configuration = new AzureAssetManagerConfiguration();
            configure(configuration);

            return configuration.DefaultDatabaseSettings.IsLocal
                ? serviceCollection.AddScoped<IAssetManager>(provider => new LocalSqlServerAssetManager(configuration))
                : serviceCollection.AddScoped<IAssetManager>(provider =>
                    new AzureAssetManager(configuration, provider.GetService<LoggerContext>()));
        }

        public static IServiceCollection AddDefaultAzureAssetManager(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var azureConfiguration = new AzureAssetManagerConfiguration();

            azureConfiguration.PortalAzureTenantUri = configuration.GetValue<Uri>("Azure:PortalAzureTenantUri");
            azureConfiguration.AppRegistrationId = configuration.GetValue<string>("Azure:Assets:AppRegistrationId");
            azureConfiguration.AppRegistrationSecret =
                configuration.GetValue<string>("Azure:Assets:AppRegistrationSecret");
            azureConfiguration.SubscriptionId = configuration.GetValue<string>("Azure:Assets:SubscriptionId");
            azureConfiguration.TenantId = configuration.GetValue<string>("Azure:Assets:TenantId");
            azureConfiguration.ResourceGroupName = configuration.GetValue<string>("Azure:Assets:ResourceGroupName");
            azureConfiguration.DefaultDatabaseSettings = configuration
                .GetSection("Azure:Assets:DefaultDatabaseSettings").Get<AzureAssetDefaultDatabaseSettings>();

            return azureConfiguration.DefaultDatabaseSettings.IsLocal
                ? serviceCollection.AddScoped<IAssetManager>(provider => new LocalSqlServerAssetManager(azureConfiguration))
                : serviceCollection.AddScoped<IAssetManager>(provider =>
                    new AzureAssetManager(azureConfiguration, provider.GetService<LoggerContext>()));
        }
    }
}