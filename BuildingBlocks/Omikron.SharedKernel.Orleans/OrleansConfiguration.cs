namespace Omikron.SharedKernel.Orleans
{
    public class OrleansConfiguration
    {
        public bool IsLocal { get; set; }
        public string AzureTableStorageConnectionString { get; set; }
        public string ClusterIdentifier { get; set; }
        public bool UseDashboard { get; set; }
    }
}