using FluentValidation;
using Omikron.IdentityService.Domain.Abstraction;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using System;

namespace Omikron.IdentityService.Domain.Commands
{
    public class GenerateTokenForNewNumber
    {
        public class Command : TenantCommand<ApiResult<string>>, IVerificationCodeSenderCommand<ApiResult<string>>
        {
            public string PhoneNumber { get; set; }
            public Guid UserId { get; set; }

            public Command(string phoneNumber, Guid userId)
            {
                PhoneNumber = phoneNumber;
                UserId = userId;
            }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^\+(?:[0-9]●?){6,14}[0-9]$").WithMessage("Invalid phone number. Valid example: +447911123456");
                RuleFor(x => x.UserId).NotEmpty();
            }
        }
    }
}
