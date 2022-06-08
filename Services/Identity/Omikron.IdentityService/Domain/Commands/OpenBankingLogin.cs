using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class OpenBankingLogin
    {
        public class Command : TenantCommand<ApiResult<ObLoginUrlResponse>>
        {
            public string UserName { get; set; }
            public string RedirectUrl { get; set; }
            public string ProviderName { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.RedirectUrl).NotEmpty().WithMessage("Redirect url should not be empty");
                RuleFor(x => x.ProviderName).NotEmpty().WithMessage("Please specify provider name");
            }
        }
    }
}
