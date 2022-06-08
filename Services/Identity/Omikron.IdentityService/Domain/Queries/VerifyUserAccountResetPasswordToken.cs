using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Queries
{
    public class VerifyUserAccountResetPasswordToken
    {
        public class Query : TenantCommand<ApiResult>
        {
            public string Token { get; set; }
        }

        public class Validation : AbstractValidator<Query>
        {
            public Validation()
            {
                RuleFor(x => x.Token).NotEmpty();
            }
        }
    }
}