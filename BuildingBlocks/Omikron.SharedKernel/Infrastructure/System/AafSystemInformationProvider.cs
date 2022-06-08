using Microsoft.AspNetCore.Http;
using Omikron.SharedKernel.Infrastructure.Data.Model;

namespace Omikron.SharedKernel.Infrastructure.System
{
    public class AafSystemInformationProvider : ISystemInformationProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AafSystemInformationProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public SystemInformation Get()
        {
            return new SystemInformation
            {
                IsUserLoggedIn = _contextAccessor.HttpContext.User.Identity.IsAuthenticated,
                Tenant = Tenant.SystemTenant.Name,
                Username = _contextAccessor.HttpContext.User.Identity.Name,
                IpAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                RequestId = _contextAccessor.HttpContext.Connection.Id
            };
        }
    }
}