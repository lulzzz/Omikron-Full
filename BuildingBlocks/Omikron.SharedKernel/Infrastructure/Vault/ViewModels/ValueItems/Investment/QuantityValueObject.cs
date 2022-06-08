namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Investment
{
    public class QuantityValueObject : BaseManualAccountDetailsVaultItem<int>
    {
        public QuantityValueObject(int value) : base("Quantity", value, false)
        {
            
        }
    }
}