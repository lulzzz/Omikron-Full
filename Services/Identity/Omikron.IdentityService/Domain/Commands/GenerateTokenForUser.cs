using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class GenerateTokenForUser
    {
        public class Command : TenantCommand<ApiResult<string>>
        {
            public string Email { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
            }
        }
    }
}
