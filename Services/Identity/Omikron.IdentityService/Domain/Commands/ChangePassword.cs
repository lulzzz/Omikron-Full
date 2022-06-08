using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class ChangePassword
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string Username { get; set; }
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
            public int VerificationToken { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.VerificationToken).NotEmpty().GreaterThan(Constants.VerificationTokenLowerBound).LessThan(Constants.VerificationTokenUpperBound).WithMessage("Invalid verification token");
                RuleFor(x => x.NewPassword).NotEmpty();
                RuleFor(x => x.CurrentPassword).NotEmpty();
            }
        }
    }
}
