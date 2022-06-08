using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.Sync.Service.Actor.States;
using System;

namespace Omikron.Sync.Service.Actor.Grains
{
	[Serializable]
    public class PropertySynchronisationGrain : BaseSynchronisationGrain<Property, PropertySynchronisationGrainState>
    {
		public PropertySynchronisationGrain(ISyncAgent<Property> syncAgent) : base(syncAgent)
		{
		}
    }
}
