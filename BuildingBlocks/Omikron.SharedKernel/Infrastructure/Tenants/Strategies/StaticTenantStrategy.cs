using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.SharedKernel.Infrastructure.Tenants.Strategies
{
    public class StaticTenantStrategy : IMultiTenantStrategy
    {
        public async Task<string> GetIdentifierAsync(object context)
        {
            return Tenant.SystemTenant.Identifier;
        }
    }
}