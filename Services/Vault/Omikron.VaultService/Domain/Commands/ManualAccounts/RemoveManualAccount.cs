using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using System;

namespace Omikron.VaultService.Domain.Commands.ManualAccounts
{
    public class RemoveManualAccount
    {
        public class Command : BaseCommand.Delete<ApiResult>
        {
            public Guid AccountId { get; set; }
            public AssetType AccountType { get; set; }
            public bool IsArchived { get; set; }
        }

        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {
                RuleFor(x => x.AccountId).NotEmpty();
                RuleFor(x => x.AccountType).NotEmpty();
                RuleFor(x => x.IsArchived).NotEmpty();
            }
        }
    }
}