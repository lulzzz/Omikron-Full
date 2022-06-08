using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Orleans;
using Omikron.Sync.Service.Actor.States;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.Service.Actor.Grains
{
	[Serializable]
	public abstract class BaseSynchronisationGrain<TEntity, TGrainState> : BaseGrain<TGrainState>, ISynchronisationGrain<TEntity> where TGrainState : BaseSynchronisationGrainState<TEntity>, new()
	{
		private readonly ISyncAgent<TEntity> _syncAgent;

		protected BaseSynchronisationGrain(ISyncAgent<TEntity> syncAgent)
		{
			_syncAgent = syncAgent;
		}

		public virtual Task<Result> InitializeEntityAsync(TEntity entity)
		{
			State.Entity = entity;
			return Task.FromResult(Result.Success());
		}

		public virtual Task<bool> IsStateInitialized()
		{
			return Task.FromResult(State.Initialized);
		}

		public virtual async Task<Result> Sync(CancellationToken cancellationToken)
		{
			await _syncAgent.DoWorkAsync(State.Entity, cancellationToken);
			return Result.Success();
		}

		public override Task OnActivateAsync()
		{
			if (!State.Initialized)
			{
				State.Initialized = true;
			}

			return base.OnActivateAsync();
		}
	}
}
