using System;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class UpdateUserAccountStatus
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Command(Guid userId, AccountStatus status)
            {
                UserId = userId;
                Status = status;
            }

            public Guid UserId { get; set; }
            public AccountStatus Status { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Status).NotEmpty().IsInEnum();
            }
        }
    }
}