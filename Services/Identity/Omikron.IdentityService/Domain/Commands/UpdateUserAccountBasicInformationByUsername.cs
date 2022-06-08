using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class UpdateUserAccountBasicInformationByUsername
    {
        public class Command : UpdateUserAccountBasicInformation.Command
        {
            public string Username { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Username).NotEmpty();
            }
        }
    }
}