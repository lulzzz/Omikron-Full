using System.Threading.Tasks;

namespace Omikron.SharedKernel.Domain
{
    public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent domainEvent);
    }
}