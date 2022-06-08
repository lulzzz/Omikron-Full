using System;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class ResetEmailTokenByUserId
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Command(Guid userId, string tenantIdentifier) : base(tenantIdentifier)
            {
                UserId = userId;
            }

            public Guid UserId { get; set; }
        }

        public class Validation : AbstractValidator<ResetEmailToken.Command>
        {
            public Validation()
            {
                RuleFor(x => x.Token).NotEmpty();
            }
        }
    }
}