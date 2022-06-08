using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Events;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Security;
using Omikron.SharedKernel.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class CreateUserCommandHandler : BaseHandler<CreateUser.Command, ApiResult>
    {
        private readonly IdentityUserManager _userManager;
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public CreateUserCommandHandler(IDispatcher dispatcher, IdentityUserManager userManager, IPhoneNumberRepository phoneNumberRepository) : base(dispatcher)
        {
            _userManager = userManager;
            _phoneNumberRepository = phoneNumberRepository;
        }

        public override async Task<ApiResult> Handle(CreateUser.Command request, CancellationToken cancellationToken)
        {
            var phoneNumber = await _phoneNumberRepository.GetPhoneNumberByNumberAsync(request.PhoneNumber, cancellationToken);
            if (phoneNumber == null)
            {
                return ApiResult.NotFound("Phone number does not exist in the database.");
            }

            var verifiedPhoneNumber = await Dispatcher.DispatchAsync(new VerifyPhoneNumber.Command(phoneNumber, request.VerificationToken), cancellationToken);
            if (!verifiedPhoneNumber.IsSuccess)
            {
                return verifiedPhoneNumber;
            }

            var user = new User
            {
                Nickname = request.Nickname,
                Email = request.Email,
                UserName = request.Email,
                PhoneNumberId = phoneNumber.Id,
                EmailConfirmed = true,
                AccountStatus = AccountStatus.PerformKyc,
                RegistrationDate = Clock.GetTime(),
                MarketingCommunications = request.EmailSubscription
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConstants.OmikronUser);
            }
            else
            {
                return ApiResult.BadRequest(result);
            }

            await _phoneNumberRepository.SaveChangesAsync(cancellationToken);

            await Dispatcher.DispatchAsync(new UserAddedEvent(user.Id), cancellationToken);

            return ApiResult.Success();
        }
    }
}
