using System.Net.Http;
using Omikron.SharedKernel.Security;
using IdentityModel.Client;
using System.Threading.Tasks;
using System;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Serialization;
using System.Threading;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;

namespace Omikron.SharedKernel.Infrastructure.Services
{
    public class HttpVaultService : IHttpVaultService
    {
		private readonly ITokenService _tokenService;
		private readonly IJsonSerialization _jsonSerializer;

        public HttpClient Client { get; }

        public HttpVaultService(HttpClient client, ITokenService tokenService, IJsonSerialization jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
            Client = client;
			_tokenService = tokenService;
        }

        public virtual async Task<ApiResult> DeleteUserRelatedData(Guid userId, CancellationToken cancellationToken = default)
        {
            Client.SetBearerToken(_tokenService.GetUserToken());
            var responseMessage = await Client.DeleteAsync($"{Client.BaseAddress.LocalPath}vault/{userId}/related-data", cancellationToken);

            if (responseMessage.IsSuccessStatusCode)
            {
                return ApiResult.NoContent();
            }

            var payload = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
            return await _jsonSerializer.DeserializeAsync<ApiResult>(payload, cancellationToken);
        }

        public virtual async Task<ApiResult<AssetPrice>> GetVehicleValue(string registration, string mileage, CancellationToken cancellationToken = default)
        {
            var payload = await Client.GetStringAsync($"{Client.BaseAddress.LocalPath}vault/vehicle-value?registration={registration}&mileage={mileage}", cancellationToken);
            var response = await _jsonSerializer.DeserializeAsync<ApiResult<AssetPrice>>(payload, cancellationToken);

            return response;
        }

        public virtual async Task<ApiResult<AssetPrice>> GetPropertyValue(string postcode, int numberOfBedrooms, CancellationToken cancellationToken = default)
        {
            var payload = await Client.GetStringAsync($"{Client.BaseAddress.LocalPath}vault/property-value?postCode={postcode}&numberOfBedrooms={numberOfBedrooms}", cancellationToken);
            var response = await _jsonSerializer.DeserializeAsync<ApiResult<AssetPrice>>(payload, cancellationToken);

            return response;
        }
    }
}
