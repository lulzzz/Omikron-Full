namespace Omikron.IdentityService.Domain.Events
{
    public class UserConfirmedEmailEvent : BaseUserEvent
    {
        public string Email { get; set; }

        public UserConfirmedEmailEvent(int userId, string email) : base(userId)
        {
            Email = email;
        }
    }
}