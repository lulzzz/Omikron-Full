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
    public class ConfirmUserAccountEmailByTokenHandler : BaseHandler<ConfirmUserAccountEmailByToken.Command, ApiResult>
    {
        private readonly OmikronIdentityDbContext _dbContext;
        private readonly IdentityUserManager _userManager;

        public ConfirmUserAccountEmailByTokenHandler(IDispatcher dispatcher, IdentityUserManager userManager, OmikronIdentityDbContext dbContext)
            : base(dispatcher: dispatcher)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public override async Task<ApiResult> Handle(ConfirmUserAccountEmailByToken.Command command, CancellationToken cancellationToken)
        {
            var query = new VerifyUserAccountConfirmationEmailToken.Query {Token = command.Token};
            var verifyTokenResult = await Dispatcher.DispatchAsync(command: query, cancellationToken: cancellationToken);

            if (!verifyTokenResult.IsSuccess)
            {
                return verifyTokenResult;
            }

            var user = await _userManager.Users
                .FirstOrDefaultAsync(predicate: t => t.ConfirmationTokens.Any(k =>
                    k.Value.Equals(command.Token) && k.Type == ConfirmationTokenType.ConfirmationEmail && k.IsExpired == false), cancellationToken: cancellationToken);

            var token = await _dbContext.ConfirmationTokens
                .FirstOrDefaultAsync(
                    predicate: t => t.Value.Equals(command.Token) && t.Type == ConfirmationTokenType.ConfirmationEmail && t.IsExpired == false, cancellationToken: cancellationToken);

            if (user == null)
            {
                return ApiResult.BadRequest("The confirmation token is not valid or is expired.");
            }

            var result = await _userManager.ConfirmEmailAsync(user: user, token: command.Token);
            if (!result.Succeeded)
            {
                return ApiResult.BadRequest(result: result);
            }

            user.AccountStatus = AccountStatus.OnBoarding;
            await _userManager.UpdateAsync(user: user);

            token.IsExpired = true;
            _dbContext.ConfirmationTokens.Update(entity: token);

            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
            await Dispatcher.DispatchAsync(domainEvent: new UserConfirmedEmailEvent(userId: user.Id, email: user.Email), cancellationToken: cancellationToken);

            return ApiResult.Success();
        }
    }
}