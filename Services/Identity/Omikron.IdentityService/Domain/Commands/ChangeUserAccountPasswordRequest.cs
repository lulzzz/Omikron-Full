using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Messaging;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class ChangeUserAccountPasswordRequest
    {
        public class Command : TenantCommand<ApiResult>
        {
            public bool BypassEmailConfirmation { get; set; }
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