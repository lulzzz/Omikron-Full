namespace Omikron.IdentityService.Domain.Events
{
    public class RoleDeletedEvent : BaseRoleEvent
    {
        public RoleDeletedEvent(int roleId) : base(roleId)
        {
        }
    }
}