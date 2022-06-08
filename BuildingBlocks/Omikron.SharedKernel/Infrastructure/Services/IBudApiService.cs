using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Services
{
    public interface IBudApiService
    {
        Task<T> GetFromApi<T>(string endpoint, string customerId = null, string customerSecret = null, CancellationToken cancellationToken = default);
        Task<T> GetFromApi<T>(string endpoint, IDictionary<string, string> headers, string customerId = null, string customerSecret = null, CancellationToken cancellationToken = default);
        Task<TResponse> PostToApi<TResponse, TRequest>(string endpoint, TRequest requestData, string customerId = null, string customerSecret = null, CancellationToken cancellationToken = default) where TRequest : class;
        Task<T> PostToApi<T>(string endpoint, string customerId = null, string customerSecret = null, CancellationToken cancellationToken = default);
        Task DeleteFromApi(string endpoint, string customerId = null, string customerSecret = null, CancellationToken cancellationToken = default);
    }
}