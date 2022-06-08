using Microsoft.Extensions.Configuration;
using Omikron.IdentityService.Models;
using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Serialization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Services
{
	public class LocationLookupService : ILocationLookupService
    {
        private readonly IConfiguration _configuration;
        private readonly IJsonSerialization _jsonSerialization;
        private readonly HttpClient _client;

        public LocationLookupService(IConfiguration configuration, IJsonSerialization jsonSerialization, HttpClient client)
        {
            _configuration = configuration;
            _jsonSerialization = jsonSerialization;
            _client = client;
        }

        public async Task<ApiResult<LocationsViewModel>> GetLocationsByPostcode(string postcode, CancellationToken cancellationToken)
        {
            var endpoint = GetLocationLookupApiUrl(postcode, false);

            var responseMessage = await _client.GetAsync(endpoint, cancellationToken);

            if (!responseMessage.IsSuccessStatusCode)
			{
                return ApiResult<LocationsViewModel>.BadRequest("Invalid postcode");
			}

            var payload = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
            var postcodeResponse = _jsonSerialization.Deserialize<PostcodeResponse>(payload);

            return ApiResult<LocationsViewModel>.Success()
                .WithData(new LocationsViewModel { Locations = postcodeResponse.Addresses.Select(a => a.Replace(" ,", "").Trim()).OrderBy(s => s.Split(" ")[0]?.ToDecimalOrDefault()) });
        }

        public async Task<PostcodeExpandedResponse> GetLocationsByPostcodeExpanded(string postcode, CancellationToken cancellationToken)
		{
            var endpoint = GetLocationLookupApiUrl(postcode, true);

            var responseMessage = await _client.GetAsync(endpoint, cancellationToken);
            var payload = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
            var postcodeResponse = _jsonSerialization.Deserialize<PostcodeExpandedResponse>(payload);

            return postcodeResponse;
        }

        private string GetLocationLookupApiUrl(string postcode, bool expanded)
		{
            var apiUrl = _configuration.GetValue<string>("ApiServices:PostcodeLookup:Url");
            var apiKey = _configuration.GetValue<string>("ApiServices:PostcodeLookup:ApiKey");
            var endpoint = $"{apiUrl}/{postcode}?api-key={apiKey}";

            return expanded ? $"{endpoint}&expand=true" : endpoint;
        }
    }
}
