using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.SharedKernel.Security
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeByTenantAndCredentials : AuthorizeAttribute, IAuthorizationFilter
    {
        private const string AuthenticationScheme = "Bearer";

        public AuthorizeByTenantAndCredentials()
        {
            AuthenticationSchemes = AuthenticationScheme;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.Any(predicate: x => x is AllowAnonymousAttribute);
            if (allowAnonymous)
            {
                return;
            }

            var tenant = Tenant.SystemTenant;
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult(authenticationScheme: AuthenticationScheme);
            }

            var clientClaim = context.HttpContext.User.Claims.FirstOrDefault(predicate: x => x.Type == Claims.ClientId);
            if (clientClaim != null && clientClaim.Value.Equals(value: Constants.ServiceClientId))
            {
                return;
            }

            var tenantIdClaim = context.HttpContext.User.Claims.FirstOrDefault(predicate: x => x.Type == Claims.TenantId);
            if (tenantIdClaim != null && tenantIdClaim.Value.Equals(value: Tenant.SystemTenant.Identifier))
            {
                return;
            }

            var tenantClaim = context.HttpContext.User.Claims.FirstOrDefault(predicate: x => x.Type == Claims.TenantId);
            if (tenantClaim == null || !tenantClaim.Value.Equals(value: tenant.Identifier))
            {
                context.Result = new ForbidResult(authenticationScheme: AuthenticationScheme);
            }
        }
    }
}