using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Repositories;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class DeleteNumberQueryHandler : BaseHandlerLight<DeleteNumber.Query, ApiResult>
    {
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public DeleteNumberQueryHandler(IPhoneNumberRepository phoneNumberRepository)
        {
            _phoneNumberRepository = phoneNumberRepository;
        }
        public override async Task<ApiResult> Handle(DeleteNumber.Query request, CancellationToken cancellationToken)
        {
            await _phoneNumberRepository.DeleteNumber(request.Number, cancellationToken);
            var result = await _phoneNumberRepository.SaveChangesAsync(cancellationToken);
            return result ? ApiResult.Success() : ApiResult.BadRequest("Invalid number");
        }
    }
}
