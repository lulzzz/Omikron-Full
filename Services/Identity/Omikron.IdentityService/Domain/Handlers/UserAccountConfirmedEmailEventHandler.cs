using System.Threading.Tasks;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Events;
using Omikron.SharedKernel.Domain;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class UserAccountConfirmedEmailEventHandler : IDomainEventHandler<UserConfirmedEmailEvent>
    {
        private readonly IDispatcher _dispatcher;
        private readonly ITenantAccessor _tenantAssessor;

        public UserAccountConfirmedEmailEventHandler(IDispatcher dispatcher, ITenantAccessor tenantAssessor)
        {
            _dispatcher = dispatcher;
            _tenantAssessor = tenantAssessor;
        }

        public async Task HandleAsync(UserConfirmedEmailEvent domainEvent)
        {
            var tenant = _tenantAssessor.GetTenant();
            await _dispatcher.DispatchAsync(new ChangeUserAccountPasswordRequest.Command {TenantIdentifier = tenant.Identifier, Email = domainEvent.Email});
        }
    }
}