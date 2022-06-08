using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Omikron.SharedKernel.Infrastructure.SecureVault.Providers
{
    public sealed class AzureKeyVaultProvider : ISecureVaultProvider
    {
        private readonly KeyVaultClient _client;
        private readonly string _keyVaultPath;

        public AzureKeyVaultProvider(IConfiguration configuration)
        {
            _keyVaultPath = configuration.GetValue<string>(key: "Azure:KeyVaultPath");
            if (string.IsNullOrWhiteSpace(value: _keyVaultPath))
            {
                throw new ArgumentNullException(paramName: "Azure:KeyVaultPath", message: "The value Azure:KeyVaultPath cannot be empty.");
            }

            var clientId = configuration.GetValue<string>(key: "Azure:AdAppRegistrationId");
            if (string.IsNullOrWhiteSpace(value: clientId))
            {
                throw new ArgumentNullException(paramName: "Azure:AdAppRegistrationId", message: "The value Azure:AdAppRegistrationId cannot be empty.");
            }

            var secret = configuration.GetValue<string>(key: "Azure:AdAppRegistrationSecret");
            if (string.IsNullOrWhiteSpace(value: secret))
            {
                throw new ArgumentNullException(paramName: "Azure:AdAppRegistrationSecret", message: "The value Azure:AdAppRegistrationSecret cannot be empty.");
            }

            _client = new KeyVaultClient(authenticationCallback: async (authority, resource, scope) =>
            {
                var adCredential = new ClientCredential(clientId: clientId, clientSecret: secret);
                var authenticationContext = new AuthenticationContext(authority: authority, tokenCache: null);
                var authenticationResult = await authenticationContext.AcquireTokenAsync(resource: resource, clientCredential: adCredential);
                return authenticationResult.AccessToken;
            });
        }

        public async Task<Maybe<string>> GetSecretAsync(string secretName)
        {
            var secret = await _client.GetSecretAsync(vaultBaseUrl: _keyVaultPath, secretName: secretName);
            return secret != null ? Maybe<string>.From(obj: secret.Value) : Maybe<string>.None;
        }

        public async Task<Maybe<X509Certificate2>> GetCertificateAsync(string certificateName)
        {
            var secret = await GetSecretAsync(secretName: certificateName);
            if (secret.HasNoValue)
            {
                return Maybe<X509Certificate2>.None;
            }

            var certBase64String = Convert.FromBase64String(s: secret.Value);
            var x509Certificate = new X509Certificate2(rawData: certBase64String);
            return Maybe<X509Certificate2>.From(obj: x509Certificate);
        }

        public Maybe<X509Certificate2> GetCertificate(string certificateName)
        {
            return GetCertificateAsync(certificateName: certificateName).GetAwaiter().GetResult();
        }
    }
}