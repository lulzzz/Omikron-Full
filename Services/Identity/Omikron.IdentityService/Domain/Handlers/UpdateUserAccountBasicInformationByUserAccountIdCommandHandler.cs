using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;

using Microsoft.EntityFrameworkCore;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class UpdateUserAccountBasicInformationByUserAccountIdCommandHandler : BaseHandler<UpdateUserAccountBasicInformationByUserAccountId.Command, ApiResult>
    {
        private readonly IdentityUserManager _userManager;

        public UpdateUserAccountBasicInformationByUserAccountIdCommandHandler(IDispatcher dispatcher,
            IdentityUserManager userManager) : base(dispatcher)
        {
            _userManager = userManager;
        }

        public override async Task<ApiResult> Handle(UpdateUserAccountBasicInformationByUserAccountId.Command command, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.ExternalId == command.Id);
            if (user == null)
            {
                return ApiResult.NotFound($"The user with id '{command.Id}' does not exist.");
            }

            var updateUserCommand = new UpdateUserAccountBasicInformationByUsername.Command()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Username = user.UserName,
                PhoneNumber = command.PhoneNumber,
                TenantIdentifier = command.TenantIdentifier
            };

            var response = Dispatcher.DispatchAsync(updateUserCommand, cancellationToken);
            return response.Result;
        }
    }
}