using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Services
{
    public interface IBudPayloadLogging
    {
        Task LogAsync(string customerId, HttpRequestMessage requestMessage, HttpResponseMessage responseMessage, CancellationToken cancellationToken);
    }
}