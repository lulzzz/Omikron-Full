using Omikron.SharedKernel.Domain;

namespace Omikron.IdentityService.Domain.Events
{
    public abstract class BaseUserEvent : BaseDomainEvent
    {
        public int UserId { get; }

        protected BaseUserEvent(int userId)
        {
            UserId = userId;
        }
    }
}
