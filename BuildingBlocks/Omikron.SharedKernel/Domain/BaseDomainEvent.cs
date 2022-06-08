using System;
using Omikron.SharedKernel.Utils;

namespace Omikron.SharedKernel.Domain
{
    public abstract class BaseDomainEvent : IDomainEvent
    {
        public BaseDomainEvent()
        {
        }

        public DateTime DateOccurred { get; protected set; } = Clock.GetTime();
    }
}