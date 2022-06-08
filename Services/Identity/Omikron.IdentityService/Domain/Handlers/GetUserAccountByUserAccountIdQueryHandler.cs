using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;

using Microsoft.EntityFrameworkCore;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class GetUserAccountByUserAccountIdQueryHandler : BaseHandlerLight<GetUserAccountByUserAccountId.Query, ApiResult<UserAccountViewModel>>
    {
        private readonly IdentityUserManager _userManager;

        public GetUserAccountByUserAccountIdQueryHandler(IdentityUserManager userManager)
        {
            _userManager = userManager;
        }

        public override async Task<ApiResult<UserAccountViewModel>> Handle(GetUserAccountByUserAccountId.Query command, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.ExternalId == command.Parameter);
            if (user == null)
            {
                throw new Exception($"The user with id '{command.Parameter}' cannot be found.");
            }

            var vm = new UserAccountViewModel(user);
            var roles = await _userManager.GetRolesAsync(user);
            vm.Roles = roles.ToArray();

            return ApiResult<UserAccountViewModel>.Success().WithData(vm);
        }
    }
}