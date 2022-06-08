namespace Omikron.IdentityService.Domain.Events
{
    public class RoleAddedEvent : BaseRoleEvent
    {
        public RoleAddedEvent(int roleId) : base(roleId)
        {
        }
    }
}