namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Property
{
    public class AddressValueObject : BaseManualAccountDetailsVaultItem<string>
    {
        public AddressValueObject(string value) : base("Address", value, false)
        {

        }
    }
}