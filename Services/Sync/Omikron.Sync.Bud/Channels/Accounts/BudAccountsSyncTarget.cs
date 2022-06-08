using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.Sync.Bud.Commands;
using Omikron.Sync.Bud.Models;
using Omikron.Sync.Model;

namespace Omikron.Sync.Bud.Channels.Accounts
{
    public class BudAccountsSyncTarget : ISyncTarget<User, CustomerAccountsData>
    {
        private readonly IDispatcher _dispatcher;

        public BudAccountsSyncTarget(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<Result> SaveAsync(User entity, SyncTargetPayload<CustomerAccountsData> syncTargetPayload, CancellationToken cancellationToken)
        {
            await _dispatcher.DispatchAsync(command: new SyncAccounts.Command(userId: entity.ExternalId, budAccounts: syncTargetPayload.Value.BudAccounts, customerConsents: syncTargetPayload.Value.Consents), cancellationToken: cancellationToken);
            return Result.Success();
        }
    }
}