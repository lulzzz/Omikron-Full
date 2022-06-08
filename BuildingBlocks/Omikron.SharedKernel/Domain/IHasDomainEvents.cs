using System.Collections.Generic;

namespace Omikron.SharedKernel.Domain
{
    public interface IHasDomainEvents
    {
        IReadOnlyList<BaseDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}