using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Omikron.SharedKernel.Infrastructure.SecureVault
{
    public interface ISecureVaultProvider
    {
        Task<Maybe<string>> GetSecretAsync(string secretName);
        Task<Maybe<X509Certificate2>> GetCertificateAsync(string certificateName);
        Maybe<X509Certificate2> GetCertificate(string certificateName);
    }
}