using Omikron.SharedKernel.Infrastructure.Commands;
using System;

namespace Omikron.VaultService.Domain.Commands.ManualAccounts
{
    public abstract class RemoveManualAccountBase : BaseCommand.Delete<bool>
    {
        public Guid AccountId { get; set; }
        public bool IsArchived { get; set; }
    }
}