using AutoMapper;
using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class GetListOfObProvidersQueryHandler : BaseHandlerLight<GetListOfObProviders.Query, ApiResult<Dictionary<char, IGrouping<char, ObProviderViewModel>>>>
    {
        private readonly IBudApiService _budApiService;
        private readonly IMapper _mapper;

        public GetListOfObProvidersQueryHandler(IBudApiService budApiService, IMapper mapper)
        {
            _budApiService = budApiService;
            _mapper = mapper;
        }

        public override async Task<ApiResult<Dictionary<char, IGrouping<char, ObProviderViewModel>>>> Handle(GetListOfObProviders.Query request, CancellationToken cancellationToken)
        {
            var response = await _budApiService.GetFromApi<BudBaseResponse<IEnumerable<ObProvidersResponse>>>(BudApiEndpoints.ListObProviders, cancellationToken: cancellationToken);

            var filterResponse = request.SearchTerm.IsNullOrWhiteSpace() ? response.Data : response.Data.Where(x => x.DisplayName.ToLower().Contains(request.SearchTerm.ToLower()));

            var result = _mapper.Map<IEnumerable<ObProviderViewModel>>(filterResponse).OrderBy(x => x.DisplayName).GroupBy(x => x.DisplayName[0]).ToDictionary(x => x.Key, x => x);

            return ApiResult<Dictionary<char, IGrouping<char, ObProviderViewModel>>>.Success().WithData(result);
        }
    }
}
