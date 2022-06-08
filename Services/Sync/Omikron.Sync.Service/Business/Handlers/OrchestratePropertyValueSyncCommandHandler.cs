using Microsoft.VisualStudio.Services.Common;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Orleans;
using Omikron.Sync.Service.Actor.Grains;
using Omikron.Sync.Service.Business.Commands;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.Service.Business.Handlers
{
	public class OrchestratePropertyValueSyncCommandHandler : BaseHandlerLight<OrchestratePropertyValueSync.Command, EmptyResult>
	{
		private const int BatchSize = 500;
		private readonly IPropertyRepository _propertyRepository;
		private readonly IGrainProvider _grainProvider;

		public OrchestratePropertyValueSyncCommandHandler(IPropertyRepository propertyRepository, IGrainProvider grainProvider)
		{
			_propertyRepository = propertyRepository;
			_grainProvider = grainProvider;
		}

		public override async Task<EmptyResult> Handle(OrchestratePropertyValueSync.Command request, CancellationToken cancellationToken)
		{
			var properties = await _propertyRepository.GetPropertiesToRevalue(cancellationToken);

			foreach (var collection in properties.Batch(BatchSize))
			{
				var tasks = collection
					.Select(property => FactorySynchronizationGrain(property, cancellationToken))
					.ToArray();

				await Task.WhenAll(tasks);
			}

			return EmptyResult.Value;
		}

		private async Task FactorySynchronizationGrain(Property property, CancellationToken cancellationToken)
		{
			var propertySynchonisationGrain = _grainProvider.GetGrain<ISynchronisationGrain<Property>>(property.Id);
			await propertySynchonisationGrain.InitializeEntityAsync(property);
			await propertySynchonisationGrain.Sync(cancellationToken);
		}
	}
}
