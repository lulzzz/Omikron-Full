using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.IdentityService.Domain.Queries
{
    public class GetLocationsByPostcode
    {
        public class Query : BaseCommand.Action<ApiResult<LocationsViewModel>>
        {
            public Query(string postcode)
            {
                Postcode = postcode;
            }

            public string Postcode { get; }
        }
    }
}
