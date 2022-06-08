namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems.Investment
{
    public class CategoryValueObject : BaseManualAccountDetailsVaultItem<string>
    {
        public CategoryValueObject(string value) : base("Category", value, false)
        {
            
        }
    }
}