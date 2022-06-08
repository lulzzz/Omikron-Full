using System;
using System.Linq;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Omikron.SharedKernel.Infrastructure.Tenants.Strategies
{
    public class DefaultTenantStrategy : IMultiTenantStrategy
    {
        public async Task<string> GetIdentifierAsync(object context)
        {
            var tenantId = StringValues.Empty;
            var allowedPath = new[] {"/api", "/connect", "/hubs"};

            if (context is HttpContext httpContext && httpContext.Request.Path.HasValue && allowedPath.Any(predicate: a => httpContext.Request.Path.ToString().StartsWith(value: a, comparisonType: StringComparison.CurrentCultureIgnoreCase)))
            {
                tenantId = httpContext.Request.Query[key: Constants.TenantIdQueryKey].ToString();

                if (string.IsNullOrWhiteSpace(value: tenantId))
                {
                    httpContext.Request.Headers.TryGetValue(key: Constants.TenantIdHeaderKey, value: out tenantId);
                }

                if (string.IsNullOrWhiteSpace(value: tenantId))
                {
                    throw new Exception(message: "The tenant id cannot be found.");
                }
            }

            return tenantId;
        }
    }
}