using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Infrastructure.Data;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Utils;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class VerifyUserAccountResetPasswordTokenHandler : BaseHandler<VerifyUserAccountResetPasswordToken.Query, ApiResult>
    {
        private readonly OmikronIdentityDbContext _dbContext;

        public VerifyUserAccountResetPasswordTokenHandler(IDispatcher dispatcher,
            OmikronIdentityDbContext dbContext) : base(dispatcher: dispatcher)
        {
            _dbContext = dbContext;
        }

        public override async Task<ApiResult> Handle(VerifyUserAccountResetPasswordToken.Query command, CancellationToken cancellationToken)
        {
            var now = Clock.GetTime();
            var valid = await _dbContext.ConfirmationTokens.AnyAsync(predicate: t =>
                t.Value.Equals(command.Token) && t.Type == ConfirmationTokenType.ResetPassword && t.Expiration >= now && t.IsExpired == false, cancellationToken: cancellationToken);

            return valid ? ApiResult.Success() : ApiResult.BadRequest("The token has been expired or is not valid.");
        }
    }
}