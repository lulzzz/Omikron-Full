using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.SharedKernel.Infrastructure.Tenants.Accessors
{
    public class TenantAccessorByStaticStrategy : ITenantAccessor
    {
        private readonly OmikronTenantInfo _tenantInfo;

        public TenantAccessorByStaticStrategy(OmikronTenantInfo tenantInfo)
        {
            _tenantInfo = tenantInfo;
        }

        public TenantAccessorByStaticStrategy() : this(tenantInfo: Tenant.SystemTenant)
        {
        }

        public OmikronTenantInfo GetTenant()
        {
            return _tenantInfo;
        }

        public OmikronTenantInfo GetTenant(string identifier)
        {
            return _tenantInfo;
        }

        public OmikronTenantInfo GetTenantOrDefault(string defaultIdentifier = null)
        {
            return _tenantInfo;
        }
    }
}