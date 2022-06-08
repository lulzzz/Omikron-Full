using System;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;
using FluentValidation;

namespace Omikron.IdentityService.Domain.Commands
{
    public class DeleteUserAccount
    {
        public class Command : TenantCommand<ApiResult>
        {
            public Command(Guid id, int verificationToken, bool isAdmin)
            {
                Id = id;
                VerificationToken = verificationToken;
                IsAdmin = isAdmin;
            }

            public Guid Id { get; set; }
            public int VerificationToken { get; set; }
            public bool IsAdmin { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.VerificationToken).NotEmpty().GreaterThan(99999).LessThan(1000000);
            }
        }
    }
}