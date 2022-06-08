namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Investment
{
    public class UnitPriceValueObject : BaseManualAccountDetailsVaultItem<decimal>
    {
        public UnitPriceValueObject(decimal value) : base("Unit Price", value, true)
        {
            
        }
    }
}