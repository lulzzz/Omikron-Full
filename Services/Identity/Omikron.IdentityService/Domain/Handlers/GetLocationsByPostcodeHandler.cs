using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Domain.Services;
using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class GetLocationsByPostcodeHandler : BaseHandlerLight<GetLocationsByPostcode.Query, ApiResult<LocationsViewModel>>
    {
        private readonly ILocationLookupService _locationLookupService;

        public GetLocationsByPostcodeHandler(ILocationLookupService locationLookupService)
        {
            _locationLookupService = locationLookupService;
        }

        public async override Task<ApiResult<LocationsViewModel>> Handle(GetLocationsByPostcode.Query request, CancellationToken cancellationToken)
        {
            return await _locationLookupService.GetLocationsByPostcode(request.Postcode, cancellationToken);
        }
    }
}
