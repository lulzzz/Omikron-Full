using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class ChangeUserAccountPasswordByToken
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string Password { get; set; }
            public string Token { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
                RuleFor(x => x.Token).NotEmpty();
            }
        }
    }
}