using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using System;

namespace Omikron.IdentityService.Domain.Commands
{
    public class ActivateRememberMe
    {
        public class Command : TenantCommand<ApiResult<string>>
        {
            public string UserName { get; set; }
            public DateTime RememberMeAt { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.UserName).NotEmpty().WithMessage("Username should not be empty");
                RuleFor(x => x.RememberMeAt).NotEmpty().WithMessage("Remember me should not be empty");
            }
        }
    }
}