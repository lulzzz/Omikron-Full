using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Omikron.Sync
{
    /// <summary>
    ///     Represent the target of where data should be stored.
    /// </summary>
    public interface ISyncTarget<TEntity, TResult>
    {
        Task<Result> SaveAsync(TEntity entity, SyncTargetPayload<TResult> syncTargetPayload, CancellationToken cancellationToken);
    }
}