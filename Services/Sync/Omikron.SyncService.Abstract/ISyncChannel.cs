using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Omikron.Sync
{
    /// <summary>
    ///     The channel of a single unit of sync.
    /// </summary>
    public interface ISyncChannel<T>
    {
        Task<Result<SyncResult>> Sync(T entity, CancellationToken cancellationToken);
    }
}