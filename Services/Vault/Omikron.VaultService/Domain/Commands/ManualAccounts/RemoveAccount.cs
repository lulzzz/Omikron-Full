using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;

namespace Omikron.VaultService.Domain.Commands.ManualAccounts
{
    public class RemoveAccount
    {
        public class Command : RemoveManualAccountBase
        {
            public AccountType AccountType { get; set; }
        }
    }
}