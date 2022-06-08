using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Extensions
{
    public static class TenantInfoExtensions
    {
        public static string GetReportingConnectionString(this OmikronTenantInfo tenantInfo) => tenantInfo.GetConnectionStringByName(Constants.ReportingConnectionString);
        public static string GetIdentityConnectionString(this OmikronTenantInfo tenantInfo) => tenantInfo.GetConnectionStringByName(Constants.IdentityConnectionString);
        public static string GetConnectionStringByName(this OmikronTenantInfo tenantInfo, TenantConnectionStringType connectionStringName)
        {
            if (tenantInfo != null && tenantInfo.Items != null && tenantInfo.Items.TryGetValue(connectionStringName, out var connectionString))
                return connectionString.ToString();

            return string.Empty;
        }
    }
}