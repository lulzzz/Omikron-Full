using System.Linq;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Property
{
    public class MortgageBalanceValueObject : BaseManualAccountDetailsVaultItem<decimal>
    {
        public MortgageBalanceValueObject(Account value) : base("Mortgage Balance", true)
        {
            if (value?.AccountBalances == null)
            {
                this.ItemValue = 0m;
                return;
            }

            this.ItemValue = value.AccountBalances.OrderByDescending(x => x.CreatedAt).First().Amount;
        }
    }
}