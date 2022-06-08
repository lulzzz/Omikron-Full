using System.Threading.Tasks;
using Coravel.Invocable;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Messaging.Sync;

namespace Omikron.Sync.Schedule.Service.Workers
{
    public class OrchestrateSyncStartWorker : IInvocable
    {
        private readonly IDispatcher _dispatcher;

        public OrchestrateSyncStartWorker(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task Invoke()
        {
            await _dispatcher.PublishEventAsync(@event: new OrchestrateSyncStartEvent());
        }
    }
}