using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync
{
    /// <summary>
    ///     The synchronization agent is responsible for orchestrating the synchronization process.
    /// </summary>
    public interface ISyncAgent<T>
    {
        Task DoWorkAsync(T entity, CancellationToken cancellationToken);
    }
}