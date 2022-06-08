namespace Omikron.SharedKernel.Messaging
{
    public class CheckTenantMigrationStatusCommand : TenantCommand, ITopicMessage
    {
        public CheckTenantMigrationStatusCommand()
        {
        }

        public CheckTenantMigrationStatusCommand(string tenantId) : base(tenantId)
        {
        }
    }
}