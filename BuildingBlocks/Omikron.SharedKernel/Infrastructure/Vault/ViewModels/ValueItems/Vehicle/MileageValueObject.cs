namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Vehicle
{
    public class MileageValueObject : BaseManualAccountDetailsVaultItem<string>
    {
        public MileageValueObject(string value) : base("Mileage", value, false)
        {
            
        }
    }
}