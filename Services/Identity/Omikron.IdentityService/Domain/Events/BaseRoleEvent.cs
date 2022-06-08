using Omikron.SharedKernel.Domain;

namespace Omikron.IdentityService.Domain.Events
{
    public abstract class BaseRoleEvent : BaseDomainEvent
    {
        public int RoleId { get; }

        protected BaseRoleEvent(int roleId)
        {
            RoleId = roleId;
        }
    }
}