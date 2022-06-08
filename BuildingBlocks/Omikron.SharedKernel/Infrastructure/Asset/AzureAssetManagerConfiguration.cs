using System;

namespace Omikron.SharedKernel.Infrastructure.Asset
{
    public class AzureAssetDefaultDatabaseSettings
    {
        public bool IsLocal { get; set; }
        public string SqlServer { get; set; }
        public string ConnectionStringTemplate { get; set; }
        public string DatabaseEditions { get; set; }
        public string ServiceObjectiveName { get; set; }
        public string DatabasePrefix { get; set; }
        public string ElasticPool { get; set; }
    }

    public class AzureAssetManagerConfiguration
    {
        public Uri PortalAzureTenantUri { get; set; }
        public string AppRegistrationId { get; set; }
        public string AppRegistrationSecret { get; set; }
        public string SubscriptionId { get; set; }
        public string TenantId { get; set; }
        public string ResourceGroupName { get; set; }

        public AzureAssetDefaultDatabaseSettings DefaultDatabaseSettings { get; set; }

        public AzureAssetManagerConfiguration()
        {
            DefaultDatabaseSettings = new AzureAssetDefaultDatabaseSettings();
        }
    }
}