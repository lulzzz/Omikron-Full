using Omikron.SharedKernel.Orleans;
using System;

namespace Omikron.Sync.Service.Actor.States
{
	[Serializable]
	public abstract class BaseSynchronisationGrainState<T> : BaseGrainState
    {
		public T Entity { get; set; }
	}
}
