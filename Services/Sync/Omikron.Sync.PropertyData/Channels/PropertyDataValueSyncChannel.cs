using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.PropertyData.Channels
{
	public class PropertyDataValueSyncChannel : ISyncChannel<Property>
	{
		private readonly LoggerContext _loggerContext;

		public PropertyDataValueSyncChannel(ISyncSource<Property, AssetPrice> source, ISyncTarget<Property, AssetPrice> target, LoggerContext loggerContext)
		{
			Source = source;
			Target = target;
			_loggerContext = loggerContext;
		}

		public ISyncSource<Property, AssetPrice> Source { get; }
		public ISyncTarget<Property, AssetPrice> Target { get; }

		public async Task<Result<SyncResult>> Sync(Property entity, CancellationToken cancellationToken)
		{
			try
			{
				_loggerContext.UsageLogger.Information(message: $"Starting sync of: {nameof(PropertyDataValueSyncChannel)}.");

				var value = await Source.FetchAsync(entity, cancellationToken);
				var targetPayload = new SyncTargetPayload<AssetPrice>(value.Value.Value);

				await Target.SaveAsync(entity, targetPayload, cancellationToken);
			}
			catch (Exception exception)
			{
				_loggerContext.UsageLogger.Error(message: $"Error occurred during sync of: {nameof(PropertyDataValueSyncChannel)}. Exception message: {exception.Message}", data: new
				{
					InnerException = exception.InnerException?.Message,
					exception.StackTrace
				});
				return new SyncResult(status: SyncStatus.Error, exception: new SyncException(message: exception.Message, inner: exception));
			}

			_loggerContext.UsageLogger.Information(message: $"Sync of: {nameof(PropertyDataValueSyncChannel)} completed successfully.");
			return new SyncResult(status: SyncStatus.Success, exception: SyncException.None);
		}
	}
}
