using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.Sync.Service.Actor.States;
using System;

namespace Omikron.Sync.Service.Actor.Grains
{
	[Serializable]
	public class VehicleSynchronisationGrain : BaseSynchronisationGrain<Vehicle, VehicleSynchronisationGrainState>
	{
		public VehicleSynchronisationGrain(ISyncAgent<Vehicle> syncAgent) : base(syncAgent)
		{
		}
	}
}
