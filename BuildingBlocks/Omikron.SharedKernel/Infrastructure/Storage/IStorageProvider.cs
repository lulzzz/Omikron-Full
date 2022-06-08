using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Storage
{
    public interface IStorageProvider<in TRequest, TResponse>
    {
        Task<TResponse> SaveAsync(TRequest blob);
        Task<bool> Delete(string blobName);
        Task<string> GetAccessToken();
    }
}