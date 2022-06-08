namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Investment
{
    public class TickerCodeValueObject : BaseManualAccountDetailsVaultItem<string>
    {
        public TickerCodeValueObject(string value) : base("Ticker Code", value, false)
        {
            
        }
    }
}