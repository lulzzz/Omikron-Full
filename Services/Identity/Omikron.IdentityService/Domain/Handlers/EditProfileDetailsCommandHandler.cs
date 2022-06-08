using AutoMapper;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Events;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class EditProfileDetailsCommandHandler : BaseHandler<EditProfileDetails.Command, ApiResult>
    {
        private readonly IdentityUserManager _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public EditProfileDetailsCommandHandler(IDispatcher dispatcher, IdentityUserManager userManager, IUserRepository userRepository, IMapper mapper) : base(dispatcher)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public override async Task<ApiResult> Handle(EditProfileDetails.Command command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdIncludePhoneNumberAsync(command.UserId, cancellationToken);
            if (user == null)
            {
                return ApiResult.NotFound($"The user '{command.UserId}' does not exist.");
            }

            var verificationCommand = new VerifyPhoneNumber.Command(user.PhoneNumberForVerification, command.VerificationToken);
            var verificationPassed = await Dispatcher.DispatchAsync(verificationCommand, cancellationToken);

            if (!verificationPassed.IsSuccess)
            {
                return ApiResult.BadRequest(verificationPassed.Errors);
            }

            _mapper.Map(command, user);

            if (user.PhoneNumberForVerification.Number != command.PhoneNumber)
            {
                user.PhoneNumberForVerification.Number = command.PhoneNumber;
            }

            var identityResult = await _userManager.UpdateAsync(user);

            if (!identityResult.Succeeded)
            {
                return ApiResult.BadRequest(identityResult);
            }

            await Dispatcher.DispatchAsync(new UserUpdatedEvent(user.Id), cancellationToken);
            return ApiResult.Success();
        }
    }
}
