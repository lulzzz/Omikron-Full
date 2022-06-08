using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Omikron.IdentityService.Infrastructure.Data;
using Omikron.IdentityService.Infrastructure.Data.Model;

namespace Omikron.IdentityService.Infrastructure.IdentityServer
{
    public class EntityFrameworkIdentityUserStore : UserStore<User, Role, OmikronIdentityDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
    {
        public EntityFrameworkIdentityUserStore(OmikronIdentityDbContext context, IdentityErrorDescriber describer = null) : base(context: context, describer: describer)
        {
        }
    }
}