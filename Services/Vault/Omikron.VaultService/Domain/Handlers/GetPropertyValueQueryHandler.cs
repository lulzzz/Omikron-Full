using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;
using Omikron.VaultService.Infrastructure.PropertyData;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class GetPropertyValueQueryHandler : BaseHandler<GetPropertyValue.Query, ApiResult<AssetValue>>
    {
        private readonly PropertyDataSettings _settings;
        private readonly HttpClient _client;

        public GetPropertyValueQueryHandler(IDispatcher dispatcher, PropertyDataSettings settings, IHttpClientFactory clientFactory) : base(dispatcher)
        {
            _settings = settings;
            _client = clientFactory.CreateClient(nameof(GetPropertyValueQueryHandler));
        }

        public override async Task<ApiResult<AssetValue>> Handle(GetPropertyValue.Query request, CancellationToken cancellationToken)
        {
            var endpoint = string.Format(_settings.PricesEndpoint, _settings.ApiKey, request.PostCode, request.NumberOfBedrooms);

            var result = await _client.GetAsync(endpoint, cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return ApiResult<AssetValue>.BadRequest(result.ReasonPhrase);
            }

            var content = await result.Content.ReadAsStringAsync(cancellationToken);
            var model = JsonSerializer.Deserialize<PropertyDataResponse>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            var price = model?.Data?.Average ?? 0;

            return ApiResult<AssetValue>.Success().WithData(AssetValue.Parse(price));
        }
    }
}