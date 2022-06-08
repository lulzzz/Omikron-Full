using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Microsoft.EntityFrameworkCore;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class
        CreateEmailConfirmationTokenHandler : BaseHandler<CreateEmailConfirmationToken.Command, ApiResult<string>>
    {
        private readonly IdentityUserManager _userManager;

        public CreateEmailConfirmationTokenHandler(IDispatcher dispatcher, IdentityUserManager userManager)
            : base(dispatcher)
        {
            _userManager = userManager;
        }

        public override async Task<ApiResult<string>> Handle(CreateEmailConfirmationToken.Command command,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(x => x.ConfirmationTokens)
                .FirstOrDefaultAsync(x => x.ExternalId == command.Id, cancellationToken);

            if (user == null)
                return ApiResult<string>.BadRequest($"The user with id '{command.Id}' does not exists.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            user.ConfirmationTokens.Add(new ConfirmationToken(ConfirmationTokenType.ConfirmationEmail, token));
            await _userManager.UpdateAsync(user);

            return ApiResult<string>.Success().WithData(token);
        }
    }
}