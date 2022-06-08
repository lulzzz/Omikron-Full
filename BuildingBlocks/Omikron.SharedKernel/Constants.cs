using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;

namespace Omikron.SharedKernel
{
    public class Constants
    {
        public static string TenantIdHeaderKey = "x-tenant-id";
        public static string TenantIdQueryKey = "tenantId";
        public static string SystemTenantId = "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8";
        public static string ServiceClientId = "omikron-service-api";
        public const string ReportingConnectionString = "ReportingConnectionString";
        public const string IdentityConnectionString = "IdentityConnectionString";
        public static string BudBearerToken = "bud-bearer-token";
        public const string TenantCachePrefixKey = "tenant.{0}";
        public static int SortCodeLength = 6;
        public static BalanceType PrimaryBalanceType = BalanceType.InterimBooked;
        public static BalanceType SecondaryBalanceType = BalanceType.InterimAvailable;
		public const int NumberOfMonthsInYear = 12;
		public const string EnglandISOCountryCode = "GB";
		public const string DefaultCurrencyCode = "GBP";
		public const int BackoffFactorInMilliseconds = 500;
    }
}