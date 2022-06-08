using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;

using Microsoft.EntityFrameworkCore;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class GetUserAccountsQueryHandler : BaseHandlerLight<GetUserAccounts.Query, ApiResult<IReadOnlyList<UserAccountViewModel>>>
    {
        private readonly IdentityUserManager _userManager;

        public GetUserAccountsQueryHandler(IdentityUserManager userManager)
        {
            _userManager = userManager;
        }

        public override async Task<ApiResult<IReadOnlyList<UserAccountViewModel>>> Handle(GetUserAccounts.Query query,
            CancellationToken cancellationToken)
        {
            var userQuery = _userManager.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                userQuery = _userManager.Users.Where(u =>
                    u.FirstName.StartsWith(query.Keyword)
                    || u.LastName.StartsWith(query.Keyword)
                    || u.NormalizedEmail.StartsWith(query.Keyword)
                    || u.NormalizedUserName.StartsWith(query.Keyword));
            }

            userQuery = userQuery
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName);

            var total = await userQuery.CountAsync();
            var users = await userQuery
                .Select(u => new UserAccountViewModel(u))
                .WithPagination(query).ToListAsync();

            return ApiResult<IReadOnlyList<UserAccountViewModel>>.Success().WithData(users).WithMeta(query, total);
        }
    }
}