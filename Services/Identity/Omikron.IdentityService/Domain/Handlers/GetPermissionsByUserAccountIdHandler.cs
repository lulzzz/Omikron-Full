using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Infrastructure.Data;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class GetPermissionsByUserAccountIdHandler : BaseHandlerLight<GetPermissionsByUserAccountId.Query, ApiResult<IReadOnlyList<string>>>
    {
        private readonly OmikronIdentityDbContext _dbContext;
        private readonly IdentityUserManager _userManager;

        public GetPermissionsByUserAccountIdHandler(OmikronIdentityDbContext dbContext, IdentityUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public override async Task<ApiResult<IReadOnlyList<string>>> Handle(GetPermissionsByUserAccountId.Query command, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(predicate: u => u.ExternalId == command.Id);
            if (user == null)
            {
                return ApiResult<IReadOnlyList<string>>.NotFound($"The user cannot be found by id '{command.Id}'");
            }

            var roles = await _userManager.GetRolesAsync(user: user);
            if (!roles.Any())
            {
                return ApiResult<IReadOnlyList<string>>.Success().WithData(result: new string[0]);
            }

            var permissions = await _dbContext.RolePermission.AsNoTracking()
                .Include(navigationPropertyPath: rp => rp.Permission)
                .Include(navigationPropertyPath: rp => rp.Role)
                .Where(predicate: rp => rp.Role.Enabled && roles.Contains(rp.Role.Name)).Select(selector: rp => rp.Permission.Name)
                .ToListAsync();

            return ApiResult<IReadOnlyList<string>>.Success().WithData(result: permissions);
        }
    }
}