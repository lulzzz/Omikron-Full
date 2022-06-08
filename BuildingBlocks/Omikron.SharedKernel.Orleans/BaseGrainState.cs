using System;

namespace Omikron.SharedKernel.Orleans
{
    [Serializable]
    public abstract class BaseGrainState
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Is this State ever initialized
        /// </summary>
        public bool Initialized { get; set; } = false;
    }
}