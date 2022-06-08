namespace Omikron.SharedKernel.Messaging
{
    /// <summary>
    ///     A event which is raised once when all assets for a tenant have been created
    /// </summary>
    public class TenantAssetsCreatedEvent : ITopicMessage
    {
        public TenantAssetsCreatedEvent()
        {
        }

        public TenantAssetsCreatedEvent(string tenantName)
        {
            TenantName = tenantName;
        }

        public string TenantName { get; set; }
    }
}