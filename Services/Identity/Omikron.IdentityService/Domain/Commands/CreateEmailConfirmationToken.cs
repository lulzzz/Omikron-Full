using System;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class CreateEmailConfirmationToken
    {
        public class Command : TenantCommand<ApiResult<string>>
        {
            public Guid Id { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }
    }
}