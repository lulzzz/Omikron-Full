using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class LoginVerification
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string UserName { get; set; }
            public int Token { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Token).GreaterThan(99999).LessThan(1000000).WithMessage("Invalid token.");
            }
        }
    }
}
