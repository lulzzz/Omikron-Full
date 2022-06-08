using System;
using System.Linq;
using Omikron.SharedKernel.Infrastructure.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Omikron.SharedKernel.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeByPermissions : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _permissions;
        private const string AuthenticationScheme = "Bearer";

        public AuthorizeByPermissions(params string [] permissions)
        {
            _permissions = permissions;
            this.AuthenticationSchemes = AuthenticationScheme;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.Any(x => x is AllowAnonymousAttribute);
            if (allowAnonymous)
            {
                return;
            }

            var clientClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == Claims.ClientId);
            if (clientClaim != null && clientClaim.Value.Equals(Constants.ServiceClientId))
            {
                return;
            }

            var userIdRouteValue = context.HttpContext.Request.RouteValues["userId"]?.ToString();
            var userIdClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == Claims.UserId);
            if (userIdClaim != null && !string.IsNullOrWhiteSpace(userIdRouteValue) && userIdRouteValue.Equals(userIdClaim.Value))
            {
                return;
            }

            var cacheManager = context.HttpContext.RequestServices.GetService<ICacheManager>();
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == Claims.UserId);
                var key = $"{userId.Value}-{Claims.Permissions}";

                var permissions = cacheManager.Get<string[]>(key) ?? new string[0];
                if (permissions.Any(r => _permissions.Any(p => r.Equals(p, StringComparison.CurrentCultureIgnoreCase))))
                {
                    return;
                }
            }
          
            context.Result = new ForbidResult(AuthenticationScheme);
        }
    }
}