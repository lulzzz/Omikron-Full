using System.Threading.Tasks;
using Coravel.Invocable;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Messaging.Sync;

namespace Omikron.Sync.Schedule.Service.Workers
{
    public class OrchestratePropertySyncStartWorker : IInvocable
	{
		private readonly IDispatcher _dispatcher;

		public OrchestratePropertySyncStartWorker(IDispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}

		public async Task Invoke()
		{
			await _dispatcher.PublishEventAsync(new OrchestratePropertyValueSyncStartEvent());
		}
	}
}
