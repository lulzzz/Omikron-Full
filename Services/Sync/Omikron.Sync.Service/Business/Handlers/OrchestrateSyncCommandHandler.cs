using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Common;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Orleans;
using Omikron.Sync.Infrastructure.ReadOnlyOmikronIdentityDatabase;
using Omikron.Sync.Model;
using Omikron.Sync.Service.Actor.Grains;
using Omikron.Sync.Service.Business.Commands;

namespace Omikron.Sync.Service.Business.Handlers
{
    public sealed class OrchestrateSyncCommandHandler : BaseHandler<OrchestrateSync.Command, EmptyResult>
    {
        private const int BatchSize = 5000;
        private readonly ReadOnlyOmikronIdentityDbContext _dbContext;
        private readonly IGrainProvider _grainProvider;

        public OrchestrateSyncCommandHandler(ReadOnlyOmikronIdentityDbContext dbContext, IGrainProvider grainProvider, IDispatcher dispatcher) : base(dispatcher: dispatcher)
        {
            _dbContext = dbContext;
            _grainProvider = grainProvider;
        }

        public override async Task<EmptyResult> Handle(OrchestrateSync.Command request, CancellationToken cancellationToken)
        {
            var users = await GetUsers(cancellationToken: cancellationToken);

            foreach (var collection in users.Batch(batchSize: BatchSize))
            {
                var tasks = collection
                    .Select(selector: user => FactorySynchronisationGrain(user: user, cancellationToken: cancellationToken))
                    .ToArray();

                await Task.WhenAll(tasks: tasks);
            }

            return EmptyResult.Value;
        }

        private async Task<List<User>> GetUsers(CancellationToken cancellationToken)
        {
            //TODO: @Adis: We should move it into SmartCache repository with cache breaker.
            return await _dbContext.Users
                .Where(u => u.BudCustomerId != null && u.BudCustomerSecret != null)
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
        }

        private async Task FactorySynchronisationGrain(User user, CancellationToken cancellationToken)
        {
            var userSynchronisationGrain = _grainProvider.GetGrain<ISynchronisationGrain<User>>(key: user.ExternalId);
            await userSynchronisationGrain.InitializeEntityAsync(entity: user);
            await userSynchronisationGrain.Sync(cancellationToken: cancellationToken);
        }
    }
}