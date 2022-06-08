using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.PropertyData.Channels
{
	public class PropertyDataValueSyncSource : ISyncSource<Property, AssetPrice>
	{
		private readonly IHttpVaultService _httpVaultService;

		public PropertyDataValueSyncSource(IHttpVaultService httpVaultService)
		{
			_httpVaultService = httpVaultService;
		}

		public async Task<Maybe<SyncSourcePayload<AssetPrice>>> FetchAsync(Property entity, CancellationToken cancellationToken)
		{
			var value = await _httpVaultService.GetPropertyValue(entity.Postcode, entity.NumberOfBedrooms, cancellationToken);

			var payload = new SyncSourcePayload<AssetPrice>(value.Records);

			return Maybe<SyncSourcePayload<AssetPrice>>.From(payload);
		}
	}
}
