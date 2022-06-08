using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;

namespace Omikron.VaultService
{
	public static class Constants
    {
        public static string ServiceName = "Omikron.VaultService";
        public static string ServiceDescription = $"Represent the {ServiceName} which is the main responsibility for managing user Vault and Accounts.";
        public const string DefaultCurrencyCode = "GBP";
        public static BalanceType PrimaryBalanceType = BalanceType.InterimBooked;
        public const string RevokeCompleted = "Completed";
    }
}