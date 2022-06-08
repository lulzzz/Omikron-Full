using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class UpdateUserAccountBasicInformation
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string LastName { get; set; }

            public string FirstName { get; set; }

            public string PhoneNumber { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).MaximumLength(50).Matches("^[a-zA-Z ]+$").WithMessage("First name can only contain alphabet letters.");
                RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).MaximumLength(50).Matches("^[a-zA-Z ]+$").WithMessage("Last name can only contain alphabet letters.");
            }
        }
    }
}