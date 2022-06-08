using Microsoft.EntityFrameworkCore;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Orleans;
using Omikron.Sync.Infrastructure.ReadOnlyOmikronIdentityDatabase;
using Omikron.Sync.Model;
using Omikron.Sync.Service.Actor.Grains;
using Omikron.Sync.Service.Business.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.Service.Business.Handlers
{
	public class OrchestrateSingleSyncCommandHandler : BaseHandler<OrchestrateSingleSync.Command, EmptyResult>
	{
		private readonly ReadOnlyOmikronIdentityDbContext _dbContext;
		private readonly IGrainProvider _grainProvider;

		public OrchestrateSingleSyncCommandHandler(IDispatcher dispatcher, ReadOnlyOmikronIdentityDbContext dbContext, IGrainProvider grainProvider) : base(dispatcher)
		{
			_dbContext = dbContext;
			_grainProvider = grainProvider;
		}

		public override async Task<EmptyResult> Handle(OrchestrateSingleSync.Command request, CancellationToken cancellationToken)
		{
			var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);

			var userSynchronisationGrain = _grainProvider.GetGrain<ISynchronisationGrain<User>>(user.ExternalId);
			await userSynchronisationGrain.InitializeEntityAsync(user);
			await userSynchronisationGrain.Sync(cancellationToken);

			return EmptyResult.Value;
		}
	}
}
