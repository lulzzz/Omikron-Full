namespace Omikron.IdentityService.Domain.Events
{
    public class UserUpdatedEvent : BaseUserEvent
    {
        public UserUpdatedEvent(int userId) : base(userId)
        {
        }
    }
}