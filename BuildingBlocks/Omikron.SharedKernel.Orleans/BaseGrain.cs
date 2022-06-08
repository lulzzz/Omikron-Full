using System.Threading.Tasks;
using Orleans;

namespace Omikron.SharedKernel.Orleans
{
    /// <summary>
    ///     Base class for a Grain with declared persistent state.
    /// </summary>
    /// <typeparam name="TGrainState">The class of the persistent state object</typeparam>
    public abstract class BaseGrain<TGrainState> : Grain<TGrainState> where TGrainState : BaseGrainState, new()
    {
        public new virtual IGrainFactory GrainFactory => base.GrainFactory;

        public new virtual TGrainState State
        {
            set => base.State = value;
            get => base.State;
        }

        public override async Task OnActivateAsync()
        {
            await InitializeState().ConfigureAwait(continueOnCapturedContext: false);

            await base.OnActivateAsync();
        }

        /// <summary>
        ///     This method is called at the end of the process of activating a grain. It is called before any messages have been
        ///     dispatched to the grain.
        /// </summary>
        protected virtual Task InitializeState()
        {
            if (!State.Initialized)
            {
                State = new TGrainState { Initialized = true };
            }

            State.Id = this.GetPrimaryKey();

            return Task.CompletedTask;
        }
    }
}