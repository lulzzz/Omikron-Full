using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
    public class AddPhoneNumber
    {
        public class Command : TenantCommand<ApiResult>
        {
            public string Number { get; set; }
            public string Email { get; set; }

            public Command(string number, string email)
            {
                Number = number;
                Email = email;
            }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Number).Matches(@"^\+(?:[0-9]●?){6,14}[0-9]$").WithMessage("Invalid phone number. Valid example: +447911123456");
                RuleFor(x => x.Email).NotNull().NotEmpty();
            }
        }
    }
}
