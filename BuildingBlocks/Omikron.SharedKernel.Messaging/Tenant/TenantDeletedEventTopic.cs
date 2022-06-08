namespace Omikron.SharedKernel.Messaging.Tenant
{
    /// <summary>
    ///     A event which is raised once when tenant has been deleted
    /// </summary>
    public class TenantDeletedEventTopic : ITopicMessage
    {
        public TenantDeletedEventTopic(string reportDatabaseName, string identityDatabaseName)
        {
            ReportDatabaseName = reportDatabaseName;
            IdentityDatabaseName = identityDatabaseName;
        }

        public string ReportDatabaseName { get; }
        public string IdentityDatabaseName { get; }
    }
}