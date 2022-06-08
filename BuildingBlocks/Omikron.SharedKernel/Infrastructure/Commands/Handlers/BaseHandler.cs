using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Omikron.SharedKernel.Infrastructure.Commands
{
    /// <summary>
    /// A base class for handlers command and queries with IDispatcher dependency. Use it when you need Dispatcher.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IDispatcher Dispatcher;

        protected BaseHandler(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}