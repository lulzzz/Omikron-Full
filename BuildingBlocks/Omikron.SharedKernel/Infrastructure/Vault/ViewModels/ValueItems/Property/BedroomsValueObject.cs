namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Property
{
    public class BedroomsValueObject : BaseManualAccountDetailsVaultItem<int>
    {
        public BedroomsValueObject(int value) : base("Bedrooms", value, false)
        {
            
        }
    }
}