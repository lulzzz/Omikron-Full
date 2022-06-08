using System.Collections.Generic;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Omikron.IdentityService.Infrastructure.IdentityServer
{
    public class IdentityRoleManager : RoleManager<Role>
    {
        public IdentityRoleManager(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger) : base(
            store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}