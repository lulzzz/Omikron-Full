using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using System;

namespace Omikron.VaultService.Domain.Queries.ManualAccountDetails
{
    public class ManualAccountDetailsBaseQuery : BaseCommand.Action<ManualAccountDetailsViewModel>
    {
        public Guid AccountId { get; set; }
    }
}