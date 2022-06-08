using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.TenantService.Domain.Services;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class ResetEmailTokenByUserIdCommandHandler : BaseHandler<ResetEmailTokenByUserId.Command, ApiResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAccountService _userService;

        public ResetEmailTokenByUserIdCommandHandler(IDispatcher dispatcher, IUserRepository userRepository, IUserAccountService userService) : base(dispatcher)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        public override async Task<ApiResult> Handle(ResetEmailTokenByUserId.Command command,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(command.UserId, cancellationToken);

            if (user == null || user.EmailConfirmed)
                return ApiResult.BadRequest("The user doesn't exist or email is confirmed for that user");

            return await CreateTokenAndSendConfirmEmail(command, user, cancellationToken);
        }

        private async Task<ApiResult<Guid>> CreateTokenAndSendConfirmEmail(ResetEmailTokenByUserId.Command command, User user,
            CancellationToken cancellationToken)
        {
            var tokenResponse = await Dispatcher.DispatchAsync(
                new CreateEmailConfirmationToken.Command
                    { Id = user.ExternalId, TenantIdentifier = command.TenantIdentifier }, cancellationToken);

            if (!tokenResponse.IsSuccess)
                return ApiResult<Guid>.BadRequest(tokenResponse.Errors);

            await _userService.SendConfirmationEmailAsync(user, tokenResponse.Records, command.TenantIdentifier);

            return ApiResult<Guid>.Success();
        }
    }
}