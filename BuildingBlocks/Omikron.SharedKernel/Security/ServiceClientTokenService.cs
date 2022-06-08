using System.Net.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace Omikron.SharedKernel.Security
{
	public class ServiceClientTokenService : ITokenService
    {
        private readonly string _clientSecret;
        private readonly IHttpContextAccessor _contextAccessor;

        public ServiceClientTokenService(IHttpClientFactory httpClientFactory, string clientSecret, IHttpContextAccessor contextAccessor)
        {
            _clientSecret = clientSecret;
            _contextAccessor = contextAccessor;
            Client = httpClientFactory.CreateClient(name: nameof(ServiceClientTokenService));
        }

        private HttpClient Client { get; }

        public string GetToken()
        {
            var request = new ClientCredentialsTokenRequest
            {
                ClientId = Constants.ServiceClientId,
                ClientSecret = _clientSecret,
                GrantType = "client_credentials"
            };
            var response = Client.RequestTokenAsync(request: request).GetAwaiter().GetResult();
            return response.AccessToken;
        }

        public string GetUserToken()
        {
            var token = _contextAccessor?.HttpContext?.Request.Headers[key: "Authorization"] ?? new StringValues();
            return token.Count > 0 ? token.FirstOrDefault()?.Split(separator: " ").LastOrDefault() : null;
        }
    }
}