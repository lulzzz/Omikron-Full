namespace Omikron.IdentityService.Domain.Events
{
    public class RoleUpdatedEvent : BaseRoleEvent
    {
        public RoleUpdatedEvent(int roleId) : base(roleId)
        {
        }
    }
}