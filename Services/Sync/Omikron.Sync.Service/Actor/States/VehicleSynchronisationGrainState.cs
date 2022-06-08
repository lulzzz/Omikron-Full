using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System;

namespace Omikron.Sync.Service.Actor.States
{
	[Serializable]
    public class VehicleSynchronisationGrainState : BaseSynchronisationGrainState<Vehicle>
	{
	}
}
