using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class CreateUser
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string Nickname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int VerificationToken { get; set; }
            public string PhoneNumber { get; set; }
            public bool EmailSubscription { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Nickname).NotEmpty().WithMessage("Nickname should not be empty.");
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
            }
        }
    }
}
