using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;
using Omikron.VaultService.Infrastructure.UkVehicleData;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class GetVehicleValueQueryHandler : BaseHandler<GetVehicleValue.Query, ApiResult<AssetValue>>
    {
        private readonly UkVehicleSettings _ukVehicleSettings;
        private readonly HttpClient _client;

        private const string ApiSuccess = "Success";

        public GetVehicleValueQueryHandler(IDispatcher dispatcher, UkVehicleSettings ukVehicleSettings, IHttpClientFactory client) : base(dispatcher)
        {
            _ukVehicleSettings = ukVehicleSettings;
            _client = client.CreateClient(nameof(GetVehicleValueQueryHandler));
        }

        public override async Task<ApiResult<AssetValue>> Handle(GetVehicleValue.Query request, CancellationToken cancellationToken)
        {
            var endpoint = string.Format(_ukVehicleSettings.ValuationEndpoint, _ukVehicleSettings.ApiKey, request.Registration, request.Mileage);
            var result = await _client.GetAsync(endpoint, cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return ApiResult<AssetValue>.BadRequest(result.ReasonPhrase);
            }

            var content = await result.Content.ReadAsStringAsync(cancellationToken);
            var response = System.Text.Json.JsonSerializer.Deserialize<UkVehicleResponse>(content);

            if (response == null) return ApiResult<AssetValue>.BadRequest();

            if (response.Response.StatusCode != ApiSuccess)
            {
                return ApiResult<AssetValue>.BadRequest(response.Response.StatusMessage);
            }

            var privateClean = response?.Response?.DataItems?.ValuationList?.PrivateClean ?? "0";

            return ApiResult<AssetValue>.Success().WithData(AssetValue.Parse(privateClean));
        }
    }
}