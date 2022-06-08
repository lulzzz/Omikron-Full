using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class GenerateTokenForUserCommandHandler : BaseHandler<GenerateTokenForUser.Command, ApiResult<string>>
    {
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public GenerateTokenForUserCommandHandler(IDispatcher dispatcher, IPhoneNumberRepository phoneNumberRepository) : base(dispatcher) 
        {
            _phoneNumberRepository = phoneNumberRepository;
        }

        public override async Task<ApiResult<string>> Handle(GenerateTokenForUser.Command request, CancellationToken cancellationToken)
        {
            var phoneNumber = await _phoneNumberRepository.GetPhoneNumberByEmailAsync(request.Email, cancellationToken);
            if (phoneNumber == null)
            {
                return ApiResult<string>.NotFound($"Phone number associated with an email {request.Email} does not exist in the system.");
            }

            var sendVerificationCodeCommand = new SendVerificationCode.Command(phoneNumber.Number, request.Email);

            var sendResult = await Dispatcher.DispatchAsync(sendVerificationCodeCommand, cancellationToken);
            if (!sendResult.IsSuccess)
            {
                return ApiResult<string>.BadRequest(sendResult.Errors);
            }

            phoneNumber.Verified = false;
            _phoneNumberRepository.UpdatePhoneNumber(phoneNumber);
            var updateResult = await _phoneNumberRepository.SaveChangesAsync(cancellationToken);

            return updateResult ? ApiResult<string>.Success().WithData(phoneNumber.Number.HidePartOfThePhoneNumber()) 
                : ApiResult<string>.BadRequest("Unexpected error occurred in a database. Please try again.");
        }
    }
}