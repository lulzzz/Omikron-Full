using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Omikron.Sync.Model;

namespace Omikron.Sync.Bud
{
    public sealed class BudSyncAgent : ISyncAgent<User>
    {
        public BudSyncAgent(IEnumerable<ISyncChannel<User>> syncChannels)
        {
            SyncChannels = syncChannels;
        }

        public IEnumerable<ISyncChannel<User>> SyncChannels { get; }

        public async Task DoWorkAsync(User user, CancellationToken cancellationToken)
        {
            foreach (var syncChannel in SyncChannels)
            {
                await syncChannel.Sync(entity: user, cancellationToken: cancellationToken);
            }
        }
	}
}