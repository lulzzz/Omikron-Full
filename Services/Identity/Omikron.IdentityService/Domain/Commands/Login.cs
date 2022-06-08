using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class Login
    {
        public class Command : TenantCommand<ApiResult<string>>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.UserName).NotEmpty().WithMessage("Username should not be empty");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password should not be empty");
            }
        }
    }
}
