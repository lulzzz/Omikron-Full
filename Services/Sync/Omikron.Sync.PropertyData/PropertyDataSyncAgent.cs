using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.Sync.PropertyData
{
	public sealed class PropertyDataSyncAgent : ISyncAgent<Property>
	{
		public PropertyDataSyncAgent(ISyncChannel<Property> syncChannel)
		{
			SyncChannels = syncChannel;
		}

		public ISyncChannel<Property> SyncChannels { get; }

		public async Task DoWorkAsync(Property entity, CancellationToken cancellationToken)
		{
			await SyncChannels.Sync(entity, cancellationToken);
		}
	}
}