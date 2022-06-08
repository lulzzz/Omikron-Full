using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Events;
using Omikron.SharedKernel.Domain;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class UserAccountAddedEventHandler : IDomainEventHandler<UserAddedEvent>
    {
        public UserAccountAddedEventHandler()
        {
        }

        public Task HandleAsync(UserAddedEvent domainEvent)
        {
            //TODO: Send email for confirmation account
            return Task.CompletedTask;
        }
    }
}
