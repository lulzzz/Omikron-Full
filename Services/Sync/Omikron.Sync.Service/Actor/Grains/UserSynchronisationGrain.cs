using System;
using Omikron.Sync.Model;
using Omikron.Sync.Service.Actor.States;

namespace Omikron.Sync.Service.Actor.Grains
{
	[Serializable]
    public class UserSynchronisationGrain : BaseSynchronisationGrain<User, UserSynchronisationGrainState>
    {
		public UserSynchronisationGrain(ISyncAgent<User> syncAgent) : base(syncAgent)
		{
		}
    }
}