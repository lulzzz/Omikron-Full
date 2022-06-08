using FluentValidation;
using Omikron.IdentityService.Domain.Abstraction;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class SendVerificationCode 
    {
        public class Command : TenantCommand<ApiResult>, IVerificationCodeSenderCommand<ApiResult>
        {
            public string PhoneNumber { get; set; }
            public string Email { get; set; }

            public Command(string number, string email)
            {
                PhoneNumber = number;
                Email = email;
            }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.PhoneNumber).Matches(@"^\+(?:[0-9]●?){6,14}[0-9]$").WithMessage("Invalid phone number. Valid example: +447911123456");
                RuleFor(x => x.Email).NotEmpty().NotNull();
            }
        }
    }
}
