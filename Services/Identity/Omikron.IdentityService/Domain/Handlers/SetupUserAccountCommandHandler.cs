using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Events;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Messaging;
using Omikron.TenantService.Domain.Services;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class SetupUserAccountCommandHandler : BaseHandler<SetupUserAccountCommand<ApiResult<Guid>>, ApiResult<Guid>>
    {
        private readonly LoggerContext _loggerContext;
        private readonly IdentityUserManager _userManager;
        private readonly IUserAccountService _userService;

        public SetupUserAccountCommandHandler(
            IDispatcher dispatcher,
            IdentityUserManager userManager,
            LoggerContext loggerContext,
            IUserAccountService userService) : base(dispatcher)
        {
            _loggerContext = loggerContext;
            _userService = userService;
            _userManager = userManager;
        }

        public override async Task<ApiResult<Guid>> Handle(SetupUserAccountCommand<ApiResult<Guid>> command,
            CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                UserName = command.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded && result.Errors.Any(e => e.Code == Constants.DuplicateUserNameErrorCode))
            {
                var errorMessage = result.Errors
                    .Where(e => e.Code == Constants.DuplicateUserNameErrorCode)
                    .Select(e => e.Description)
                    .FirstOrDefault();

                return ApiResult<Guid>.BadRequest(errorMessage);
            }

            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine,
                    result.Errors.Select(e => $"Create Account Error: {e.Code} - {e.Description}"));
                _loggerContext.ErrorLogger.Error(errors);
                return ApiResult<Guid>.BadRequest(errors);
            }

            var tokenResponse = await Dispatcher.DispatchAsync(
                new CreateEmailConfirmationToken.Command
                    {Id = user.ExternalId, TenantIdentifier = command.TenantIdentifier}, cancellationToken);
            if (!tokenResponse.IsSuccess)
            {
                return ApiResult<Guid>.BadRequest(tokenResponse.Errors);
            }

            var token = tokenResponse.Records;

            if (command.Roles != null && command.Roles.Any()) await _userManager.AddToRolesAsync(user, command.Roles);

            await Dispatcher.DispatchAsync(new UserAddedEvent(user.Id), cancellationToken);

            return ApiResult<Guid>.Success().WithData(user.ExternalId);
        }
    }
}