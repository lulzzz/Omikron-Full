using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Serialization;
using Omikron.SharedKernel.Security;
using IdentityModel.Client;
using System.Threading;

namespace Omikron.SharedKernel.Infrastructure.Services
{
	public class HttpIdentityService : IHttpIdentityService
	{
		private readonly ITokenService _tokenService;
		private readonly IJsonSerialization _jsonSerializer;
		public HttpClient Client { get; }

		public HttpIdentityService(HttpClient client, ITokenService tokenService, IJsonSerialization jsonSerializer)
		{
			_jsonSerializer = jsonSerializer;

			Client = client;
			_tokenService = tokenService;
		}

		public virtual async Task<ApiResult<TResponse>> GetUserRegistrationDate<TResponse>(CancellationToken cancellationToken = default)
		{
			Client.SetBearerToken(_tokenService.GetUserToken());
			var payload = await Client.GetStringAsync($"{Client.BaseAddress.LocalPath}user/registration-date", cancellationToken);
			var response = await _jsonSerializer.DeserializeAsync<ApiResult<TResponse>>(payload, cancellationToken);

			return response;
		}

		public virtual async Task<ApiResult<TResponse>> GetUserByUsername<TResponse>(string username, CancellationToken cancellationToken = default)
		{
			Client.SetBearerToken(_tokenService.GetUserToken());
			var payload = await Client.GetStringAsync($"{Client.BaseAddress.LocalPath}user/search/{username}", cancellationToken);
			var response = await _jsonSerializer.DeserializeAsync<ApiResult<TResponse>>(payload, cancellationToken);

			return response;
		}

		public virtual async Task<ApiResult<TResponse>> GetUsersByRoles<TResponse>(string[] roles, string tenantId)
		{
			Client.SetBearerToken(_tokenService.GetUserToken());
			var query = string.Join("&", roles.Select(r => $"Parameter={r}"));

			var payload = await Client.GetStringAsync($"{Client.BaseAddress.LocalPath}user/search/roles?{query}&{Constants.TenantIdQueryKey}={tenantId}");
			var response = _jsonSerializer.Deserialize<ApiResult<TResponse>>(payload);

			return response;
		}

		public virtual async Task<ApiResult> DeleteAccount<TResponse>(Guid userId, string tenantId)
		{
			Client.SetBearerToken(_tokenService.GetUserToken());
			var responseMessage = await Client.DeleteAsync($"{Client.BaseAddress.LocalPath}user/{userId}?{Constants.TenantIdQueryKey}={tenantId}");

			if (responseMessage.IsSuccessStatusCode)
				return ApiResult.NoContent();

			var payload = await responseMessage.Content.ReadAsStringAsync();
			var response = _jsonSerializer.Deserialize<ApiResult>(payload);
			return response;
		}

		public virtual async Task<ApiResult> CreateUserAccount<TRequest>(TRequest request, string tenantId)
		{
			Client.SetBearerToken(_tokenService.GetUserToken());
			return await SendUserAccountCommand(request, tenantId, HttpMethod.Post);
		}

		public virtual async Task<ApiResult> UpdateUserAccount<TRequest>(TRequest request, string tenantId)
		{
			Client.SetBearerToken(_tokenService.GetUserToken());
			return await SendUserAccountCommand(request, tenantId, HttpMethod.Put);
		}

		private async Task<ApiResult> SendUserAccountCommand<TRequest>(TRequest request, string tenantId, HttpMethod method)
		{
			Client.SetBearerToken(_tokenService.GetUserToken());
			var url = $"{Client.BaseAddress.LocalPath}user?{Constants.TenantIdQueryKey}={tenantId}";

			var payload = JsonSerializer.Serialize(request);
			HttpResponseMessage responseMessage = null;

			var content = new StringContent(payload, Encoding.UTF8, "application/json");

			if (HttpMethod.Post == method)
			{
				responseMessage = await Client.PostAsync(url, content);
			}
			else if (HttpMethod.Put == method)
			{
				responseMessage = await Client.PutAsync(url, content);
			}

			return ApiResult.FromHttpResponse(responseMessage);
		}
	}
}