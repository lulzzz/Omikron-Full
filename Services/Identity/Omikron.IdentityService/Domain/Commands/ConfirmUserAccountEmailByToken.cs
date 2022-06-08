using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class ConfirmUserAccountEmailByToken
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string Token { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Token).NotEmpty();
            }
        }
    }
}