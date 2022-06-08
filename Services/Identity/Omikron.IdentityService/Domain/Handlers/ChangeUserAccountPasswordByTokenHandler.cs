using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Events;
using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Infrastructure.Data;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class ChangeUserAccountPasswordByTokenHandler : BaseHandler<ChangeUserAccountPasswordByToken.Command, ApiResult>
    {
        private readonly OmikronIdentityDbContext _dbContext;
        private readonly IdentityUserManager _userManager;

        public ChangeUserAccountPasswordByTokenHandler(IDispatcher dispatcher, IdentityUserManager userManager, OmikronIdentityDbContext dbContext)
            : base(dispatcher: dispatcher)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public override async Task<ApiResult> Handle(ChangeUserAccountPasswordByToken.Command command, CancellationToken cancellationToken)
        {
            var query = new VerifyUserAccountResetPasswordToken.Query {Token = command.Token};
            var verifyTokenResult = await Dispatcher.DispatchAsync(command: query, cancellationToken: cancellationToken);

            if (!verifyTokenResult.IsSuccess)
            {
                return verifyTokenResult;
            }

            var user = await _userManager.Users
                .FirstOrDefaultAsync(predicate: t => t.ConfirmationTokens.Any(k =>
                    k.Value.Equals(command.Token) && k.Type == ConfirmationTokenType.ResetPassword && k.IsExpired == false), cancellationToken: cancellationToken);

            var token = await _dbContext.ConfirmationTokens
                .FirstOrDefaultAsync(
                    predicate: t => t.Value.Equals(command.Token) && t.Type == ConfirmationTokenType.ResetPassword && t.IsExpired == false, cancellationToken: cancellationToken);

            var identityResult = await _userManager.ResetPasswordAsync(user: user, token: command.Token, newPassword: command.Password);

            token.IsExpired = true;
            _dbContext.ConfirmationTokens.Update(entity: token);

            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);

            if (!identityResult.Succeeded)
            {
                return ApiResult.BadRequest(result: identityResult);
            }

            await Dispatcher.DispatchAsync(domainEvent: new UserPasswordChangedEvent(userId: user.Id), cancellationToken: cancellationToken);
            return ApiResult.Success();
        }
    }
}