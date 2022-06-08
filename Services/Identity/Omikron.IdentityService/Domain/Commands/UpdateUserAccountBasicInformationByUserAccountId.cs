using System;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class UpdateUserAccountBasicInformationByUserAccountId
    {
        public class Command : UpdateUserAccountBasicInformationByUsername.Command
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