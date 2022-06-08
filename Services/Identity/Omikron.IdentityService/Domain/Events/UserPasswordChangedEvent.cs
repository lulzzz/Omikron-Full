namespace Omikron.IdentityService.Domain.Events
{
    public class UserPasswordChangedEvent : BaseUserEvent
    {
        public UserPasswordChangedEvent(int userId) : base(userId)
        {
        }
    }
}