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
	public class OrchestrateVehicleValueSyncCommandHandler : BaseHandlerLight<OrchestrateVehicleValueSync.Command, EmptyResult>
	{
		private const int BatchSize = 500;
		private readonly IVehicleRepository _vehicleRepository;
		private readonly IGrainProvider _grainProvider;

		public OrchestrateVehicleValueSyncCommandHandler(IVehicleRepository vehicleRepository, IGrainProvider grainProvider)
		{
			_vehicleRepository = vehicleRepository;
			_grainProvider = grainProvider;
		}

		public override async Task<EmptyResult> Handle(OrchestrateVehicleValueSync.Command request, CancellationToken cancellationToken)
		{
			var vehicles = await _vehicleRepository.GetVehiclesToRevalue(cancellationToken);

			foreach (var collection in vehicles.Batch(BatchSize))
			{
				var tasks = collection
					.Select(vehicle => FactorySynchronizationGrain(vehicle, cancellationToken))
					.ToArray();

				await Task.WhenAll(tasks);
			}

			return EmptyResult.Value;
		}

		private async Task FactorySynchronizationGrain(Vehicle vehicle, CancellationToken cancellationToken)
		{
			var vehicleSynchonisationGrain = _grainProvider.GetGrain<ISynchronisationGrain<Vehicle>>(vehicle.Id);
			await vehicleSynchonisationGrain.InitializeEntityAsync(vehicle);
			await vehicleSynchonisationGrain.Sync(cancellationToken);
		}
	}
}
