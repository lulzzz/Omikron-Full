namespace Omikron.SharedKernel.Messaging
{
    public class SetupTenantDatabaseCommand : TenantCommand, ITopicMessage
    {
        public SetupTenantDatabaseCommand()
        {
        }

        public SetupTenantDatabaseCommand(string tenantId) : base(tenantId)
        {
        }
    }
}