namespace Omikron.SharedKernel.Messaging
{
    public class SetupUserAccountCommand<TResponse> : TenantCommand<TResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string [] Roles { get; set; }

        public SetupUserAccountCommand()
        {
        }

        public SetupUserAccountCommand(string firstName, string lastName, string email, string[] roles)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Roles = roles;
        }
    }
}