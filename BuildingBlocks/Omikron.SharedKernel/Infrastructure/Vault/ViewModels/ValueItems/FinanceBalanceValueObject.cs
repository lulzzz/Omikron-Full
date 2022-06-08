using System.Linq;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems
{
    public class FinanceBalanceValueObject : BaseManualAccountDetailsVaultItem<decimal>
    {
        public FinanceBalanceValueObject(Account value) : base("Finance Balance", true)
        {
            if (value?.AccountBalances == null || !value.AccountBalances.Any())
            {
                this.ItemValue = 0m;
                return;
            }

            this.ItemValue = value.AccountBalances.OrderByDescending(x => x.CreatedAt).First().Amount;
        }
    }
}