using System;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Microsoft.Extensions.Hosting;

namespace Omikron.SharedKernel.Infrastructure.Jobs
{
    public class QueuedHostedService : BackgroundService
    {
        private readonly LoggerContext _logger;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue, LoggerContext logger)
        {
            TaskQueue = taskQueue;
            _logger = logger;
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.UsageLogger.Information("Queued Asset Creation Hosted Service is starting.");

            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);

                try
                {
                    await workItem(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.ErrorLogger.Error(ex, $"Error occurred executing {nameof(workItem)}.");
                }
            }

            _logger.UsageLogger.Information("Queued Asset Creation Hosted Service is stopping.");
        }
    }
}