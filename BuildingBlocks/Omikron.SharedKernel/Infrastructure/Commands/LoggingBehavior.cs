using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using MediatR;
               
namespace Omikron.SharedKernel.Infrastructure.Commands
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly LoggerContext _loggerContext;

        public LoggingBehavior(LoggerContext loggerContext)
        {
            _loggerContext = loggerContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _loggerContext.UsageLogger.Information($"Command '{request.GetType().FullName}' has been executed.");
            return await next();
        }
    }
}