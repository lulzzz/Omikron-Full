using Omikron.IdentityService.Models;
using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Services
{
    public interface ILocationLookupService
    {
        Task<ApiResult<LocationsViewModel>> GetLocationsByPostcode(string postcode, CancellationToken cancellationToken);
        Task<PostcodeExpandedResponse> GetLocationsByPostcodeExpanded(string postcode, CancellationToken cancellationToken);
    }
}