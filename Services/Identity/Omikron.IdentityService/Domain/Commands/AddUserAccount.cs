using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Messaging;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class AddUserAccount
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string[] Roles { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).MaximumLength(50).Matches("^[a-zA-Z ]+$").WithMessage("First name can only contain alphabet letters.");
                RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).MaximumLength(50).Matches("^[a-zA-Z ]+$").WithMessage("Last name can only contain alphabet letters.");
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
            }
        }
    }
}