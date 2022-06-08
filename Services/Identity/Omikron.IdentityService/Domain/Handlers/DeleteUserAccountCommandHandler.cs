using Microsoft.EntityFrameworkCore;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Events;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class DeleteUserAccountCommandHandler : BaseHandler<DeleteUserAccount.Command, ApiResult>
    {
        private readonly IdentityUserManager _userManager;
        private readonly IHttpVaultService _httpVaultService;
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public DeleteUserAccountCommandHandler(IDispatcher dispatcher, IdentityUserManager userManager, IHttpVaultService httpVaultService,
            IPhoneNumberRepository phoneNumberRepository) :base(dispatcher)
        {
            _userManager = userManager;
            _httpVaultService = httpVaultService;
            _phoneNumberRepository = phoneNumberRepository;
        }

        public override async Task<ApiResult> Handle(DeleteUserAccount.Command command, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.AsNoTracking().SingleOrDefaultAsync(u => u.ExternalId == command.Id, cancellationToken);
            if (user == null)
            {
                return ApiResult.NotFound($"The user cannot be found by id '{command.Id}'");
            }

            var phoneNumber = await _phoneNumberRepository.GetPhoneNumberByIdAsync(user.PhoneNumberId, cancellationToken);
            if (phoneNumber == null)
            {
                return ApiResult.BadRequest("There has been a problem with deleting your account. Please try again.");
            }

            if(!command.IsAdmin)
            {
                var verifiedPhoneNumber = await Dispatcher.DispatchAsync(new VerifyPhoneNumber.Command(phoneNumber, command.VerificationToken), cancellationToken);
                if (!verifiedPhoneNumber.IsSuccess)
                {
                    return verifiedPhoneNumber;
                }
            }

            await _phoneNumberRepository.DeleteNumber(phoneNumber.Number, cancellationToken);
            var deleteSucceeded = await _phoneNumberRepository.SaveChangesAsync(cancellationToken);

            if (!deleteSucceeded)
            {
                return ApiResult.BadRequest("There has been a problem with deleting your account. Please try again.");
            }

            await _httpVaultService.DeleteUserRelatedData(user.ExternalId, cancellationToken);

            await Dispatcher.DispatchAsync(new UserDeletedEvent(user.Id), cancellationToken);
            return ApiResult.Success();
        }
    }
}