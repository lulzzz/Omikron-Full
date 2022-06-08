using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Omikron.Sync
{
    /// <summary>
    ///     Represent the souroce of data what should be collected.
    /// </summary>
    public interface ISyncSource<TEntity, TResult>
    {
        Task<Maybe<SyncSourcePayload<TResult>>> FetchAsync(TEntity entity, CancellationToken cancellationToken);
    }
}