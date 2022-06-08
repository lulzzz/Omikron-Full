using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class ResetPassword
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public int VerificationToken { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.VerificationToken).NotEmpty().GreaterThan(Constants.VerificationTokenLowerBound).LessThan(Constants.VerificationTokenUpperBound);
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
}
