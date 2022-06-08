using Omikron.IdentityService.Domain.Commands;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class AddPhoneNumberCommandHandler : BaseHandler<AddPhoneNumber.Command, ApiResult>
    {
        public AddPhoneNumberCommandHandler(IDispatcher dispatcher) : base(dispatcher)
        {

        }

        public override async Task<ApiResult> Handle(AddPhoneNumber.Command request, CancellationToken cancellationToken)
        {
            return await Dispatcher.DispatchAsync(new SendVerificationCode.Command(request.Number, request.Email), cancellationToken);
        }
    }
}