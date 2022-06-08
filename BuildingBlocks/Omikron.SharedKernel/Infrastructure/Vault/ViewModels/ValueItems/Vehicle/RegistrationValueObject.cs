namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Vehicle
{
    public class RegistrationValueObject : BaseManualAccountDetailsVaultItem<string>
    {
        public RegistrationValueObject(string value) : base("Registration", value, false)
        {
            
        }
    }
}