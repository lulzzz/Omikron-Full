namespace Omikron.IdentityService.Domain.Events
{
    public class UserDeletedEvent : BaseUserEvent
    {
        public UserDeletedEvent(int userId) : base(userId)
        {
        }
    }
}