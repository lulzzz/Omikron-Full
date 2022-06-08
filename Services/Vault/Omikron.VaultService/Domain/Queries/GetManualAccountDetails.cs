using System;
using FluentValidation;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;

namespace Omikron.VaultService.Domain.Queries
{
    public class GetManualAccountDetails
    {
        public class Query : BaseCommand.Action<ApiResult<ManualAccountDetailsViewModel>>
        {
            public Guid AccountId { get; set; }
            public AssetType ItemType { get; set; }
            public Guid FinanceId { get; set; }
        }

        public class Validation : AbstractValidator<Query>
        {
            public Validation()
            {
                RuleFor(x => x.AccountId).NotEmpty();
                RuleFor(x => x.ItemType).NotEmpty();
            }
        }
    }
}