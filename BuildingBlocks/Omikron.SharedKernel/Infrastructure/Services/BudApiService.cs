using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Omikron.SharedKernel.Exceptions;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Cache;
using Omikron.SharedKernel.Infrastructure.Configuration;
using Omikron.SharedKernel.Infrastructure.Data.Model;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Infrastructure.Serialization;
using Omikron.SharedKernel.Utils;

namespace Omikron.SharedKernel.Infrastructure.Services
{

	public class BudApiService : IBudApiService
    {
        private readonly IBudPayloadLogging _budPayloadLogging;
        private readonly ICacheManager _cacheManager;
		private readonly LoggerContext _logger;
		private readonly BudConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IJsonSerialization _jsonSerialization;

        public BudApiService(HttpClient httpClient,
            IBudPayloadLogging budPayloadLogging,
            IJsonSerialization jsonSerialization,
            IOptionsMonitor<BudConfiguration> options,
            ICacheManager cacheManager,
            LoggerContext logger)
        {
            _httpClient = httpClient;
            _budPayloadLogging = budPayloadLogging;
            _jsonSerialization = jsonSerialization;
            _configuration = options.CurrentValue;
            _cacheManager = cacheManager;
			_logger = logger;
		}

        public async Task<T> GetFromApi<T>(string endpoint,
            string customerId = null,
            string customerSecret = null,
            CancellationToken cancellationToken = default)
        {
            return await SendToApi<T>(httpMethod: HttpMethod.Get, endpoint: endpoint, customerId: customerId, customerSecret: customerSecret, cancellationToken: cancellationToken);
        }

        public async Task<T> GetFromApi<T>(string endpoint,
            IDictionary<string, string> headers,
            string customerId = null,
            string customerSecret = null,
            CancellationToken cancellationToken = default)
        {
            return await SendToApi<T>(httpMethod: HttpMethod.Get, endpoint: endpoint, headers: headers, customerId: customerId, customerSecret: customerSecret, cancellationToken: cancellationToken);
        }

        public async Task<T> PostToApi<T>(string endpoint,
            string customerId = null,
            string customerSecret = null,
            CancellationToken cancellationToken = default)
        {
            return await SendToApi<T>(httpMethod: HttpMethod.Post, endpoint: endpoint, customerId: customerId, customerSecret: customerSecret, cancellationToken: cancellationToken);
        }

        public async Task<TResponse> PostToApi<TResponse, TRequest>(string endpoint,
            TRequest requestData,
            string customerId = null,
            string customerSecret = null,
            CancellationToken cancellationToken = default) where TRequest : class
        {
            return await SendToApi<TResponse, TRequest>(httpMethod: HttpMethod.Post, endpoint: endpoint, requestData: requestData, customerId: customerId, customerSecret: customerSecret, cancellationToken: cancellationToken);
        }

        public async Task DeleteFromApi(string endpoint,
            string customerId = null,
            string customerSecret = null,
            CancellationToken cancellationToken = default)
        {
            await SendToApi(httpMethod: HttpMethod.Delete, endpoint: endpoint, customerId: customerId, customerSecret: customerSecret, cancellationToken: cancellationToken);
        }

        private async Task SendToApi(HttpMethod httpMethod,
            string endpoint,
            string customerId = null,
            string customerSecret = null,
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = httpMethod
            };

            await GetResponse(request: request, endpoint: endpoint, customerId: customerId, customerSecret: customerSecret, cancellationToken: cancellationToken);
        }

        private async Task<T> SendToApi<T>(HttpMethod httpMethod,
            string endpoint,
            string customerId = null,
            string customerSecret = null,
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = httpMethod
            };

            var response = await GetResponse(request: request, endpoint: endpoint, customerId: customerId, customerSecret: customerSecret, cancellationToken: cancellationToken);
            return await ValidateAndDeserialize<T>(responseMessage: response, cancellationToken: cancellationToken);
        }

        private async Task<T> SendToApi<T>(HttpMethod httpMethod,
            string endpoint,
            IDictionary<string, string> headers,
            string customerId = null,
            string customerSecret = null,
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = httpMethod
            };

            foreach (var header in headers)
            {
                request.Headers.Add(name: header.Key, value: header.Value);
            }

            var response = await GetResponse(request: request, endpoint: endpoint, customerId: customerId, customerSecret: customerSecret, cancellationToken: cancellationToken);
            return await ValidateAndDeserialize<T>(responseMessage: response, cancellationToken: cancellationToken);
        }

        private async Task<TResponse> SendToApi<TResponse, TRequest>(HttpMethod httpMethod,
            string endpoint,
            TRequest requestData,
            string customerId = null,
            string customerSecret = null,
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = httpMethod,
                Content = new StringContent(content: await _jsonSerialization.SerializeAsync(value: requestData, cancellationToken: cancellationToken))
            };

            var response = await GetResponse(request: request, endpoint: endpoint, customerId: customerId, customerSecret: customerSecret, cancellationToken: cancellationToken);
            return await ValidateAndDeserialize<TResponse>(responseMessage: response, cancellationToken: cancellationToken);
        }

        private async Task<HttpResponseMessage> GetResponse(HttpRequestMessage request, string endpoint, string customerId, string customerSecret, CancellationToken cancellationToken)
        {
			var bearer = _cacheManager.Get<string>(key: Constants.BudBearerToken) ?? await GetBearerTokenFromBud(cancellationToken: cancellationToken);

			request.RequestUri = new Uri(uriString: $"{_configuration.ApiUrl}{endpoint}");
            request.Headers.Add(name: "Authorization", value: $"Bearer {bearer}");
            request.Headers.Add(name: "X-Client-Id", value: _configuration.ClientId);

            if (customerId != null)
            {
                request.Headers.Add(name: "X-Customer-Id", value: customerId);
            }

            if (customerSecret != null)
            {
                request.Headers.Add(name: "X-Customer-Secret", value: customerSecret);
            }

            var response = await _httpClient.SendAsync(request: request, cancellationToken: cancellationToken);

            await _budPayloadLogging.LogAsync(customerId: customerId, requestMessage: request, responseMessage: response, cancellationToken: cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var error = await ValidateAndDeserialize<BudErrorResponse>(responseMessage: response, cancellationToken: cancellationToken);
                _logger.UsageLogger.Error(message: $"Error occurred during API action: {request.RequestUri.AbsoluteUri} with status code: {response.StatusCode} and message {error.Message}", error);

                throw new BudApiException(message: $"We experienced unexpected issues. Please contact the administrator.");
            }

            return response;
        }

        private async Task<string> GetBearerTokenFromBud(CancellationToken cancellationToken = default)
        {
            var requestUri = $"{_configuration.ApiUrl}{BudApiEndpoints.Authentication}";
            var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: requestUri);

            var data = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(key: "grant_type", value: BudGrantType.ClientCredentials)
            };

            var authorization = $"{_configuration.ClientId}:{_configuration.ClientSecret}".ToBase64();
            request.Headers.Add(name: "Authorization", value: $"Basic {authorization}");

            request.Content = new FormUrlEncodedContent(nameValueCollection: data);

            var response = await _httpClient.SendAsync(request: request, cancellationToken: cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new BudApiException(message: $"Error occurred during API action: {requestUri} with status code: {response.StatusCode}");
            }

            var accessTokenData = await ValidateAndDeserialize<BudBaseResponse<AuthenticationResponse>>(responseMessage: response, cancellationToken: cancellationToken);

            _cacheManager.Set(key: Constants.BudBearerToken, @object: accessTokenData.Data.AccessToken,
                expired: CacheExpirationTime.BudAccessTokenExpiration(valueInSeconds: accessTokenData.Data.ExpiresIn));

            return accessTokenData.Data.AccessToken;
        }

        private async Task<T> ValidateAndDeserialize<T>(HttpResponseMessage responseMessage, CancellationToken cancellationToken = default)
        {
            var json = await responseMessage.Content.ReadAsStringAsync(cancellationToken: cancellationToken);

            if (json.IsNullOrWhiteSpace())
            {
                throw new BudApiException(message: "Api returned null or empty string");
            }

            try
            {
                return _jsonSerialization.Deserialize<T>(value: json);
            }
            catch (Exception ex)
            {
                throw new SerializationException(message: $"An error occurred while deserializing input {typeof(T)}", innerException: ex);
            }
        }
    }
}