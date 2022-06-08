namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels.ValueItems
{
    public interface IBaseManualAccountDetailsVaultItem
    {
        string Name { get; }
        bool RenderCurrency { get; }
        object Value { get; }
    }
}