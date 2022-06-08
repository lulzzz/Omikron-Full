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
    public class VerifyUserAccountConfirmationEmailTokenHandler : BaseHandlerLight<VerifyUserAccountConfirmationEmailToken.Query, ApiResult>
    {
        private readonly OmikronIdentityDbContext _dbContext;

        public VerifyUserAccountConfirmationEmailTokenHandler(OmikronIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<ApiResult> Handle(VerifyUserAccountConfirmationEmailToken.Query command, CancellationToken cancellationToken)
        {
            var now = Clock.GetTime();
            var valid = await _dbContext.ConfirmationTokens.AnyAsync(predicate: t =>
                t.Value.Equals(command.Token) && t.Type == ConfirmationTokenType.ConfirmationEmail &&
                t.Expiration >= now, cancellationToken: cancellationToken);

            return valid ? ApiResult.Success() : ApiResult.BadRequest("The token has been expired or is not valid.");
        }
    }
}