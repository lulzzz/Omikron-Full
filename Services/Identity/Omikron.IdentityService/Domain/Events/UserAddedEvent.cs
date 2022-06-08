namespace Omikron.IdentityService.Domain.Events
{
    public class UserAddedEvent : BaseUserEvent
    {
        public UserAddedEvent(int userId) : base(userId)
        {
        }
    }
}