using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.SharedKernel.Infrastructure.Tenants
{
    public class StaticTenantHolder : ITenantHolder
    {
        public void SetTenant(string tenant)
        {
        }

        public string GetTenant()
        {
            return Tenant.SystemTenant.Identifier;
        }
    }
}