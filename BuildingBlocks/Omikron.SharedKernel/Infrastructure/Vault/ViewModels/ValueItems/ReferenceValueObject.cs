using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems
{
    public class ReferenceValueObject : BaseManualAccountDetailsVaultItem<string>
    {
        public ReferenceValueObject(Account value) : base("Reference", false)
        {
            if (value == null)
            {
                this.ItemValue = "";
                return;
            }

            this.ItemValue = value.ReferenceNumber;
        }
    }
}