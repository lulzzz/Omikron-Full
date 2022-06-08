using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class LoginCommandHandler : BaseHandler<Login.Command, ApiResult<string>>
    {
        private readonly IdentityUserManager _userManager;
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public LoginCommandHandler(IDispatcher dispatcher, IdentityUserManager userManager, IPhoneNumberRepository phoneNumberRepository) : base(dispatcher)
        {
            _userManager = userManager;
            _phoneNumberRepository = phoneNumberRepository;
        }

        public override async Task<ApiResult<string>> Handle(Login.Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return ApiResult<string>.BadRequest($"User with username: {request.UserName} does not exist.");
            }

            var passwordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordCorrect)
            {
                return ApiResult<string>.BadRequest($"Invalid password.");
            }

            var validateUsersRememberMeData = (!user.RememberMeAt.HasValue || ((Clock.GetTime() - user.RememberMeAt).Value.TotalDays > Constants.RememberMeValidDays));
            if (validateUsersRememberMeData)
            {
                var number = await _phoneNumberRepository.GetPhoneNumberByEmailAsync(user.UserName, cancellationToken);
                var sendCode = await Dispatcher.DispatchAsync(new SendVerificationCode.Command(number.Number, request.UserName), cancellationToken);
                if (!sendCode.IsSuccess)
                {
                    return ApiResult<string>.BadRequest(sendCode.Errors);
                }

                return ApiResult<string>.Success().WithData(number.Number.HidePartOfThePhoneNumber());
            }

            return ApiResult<string>.Success().WithData("RememberMeActivated");
        }
    }
}