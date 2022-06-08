using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Omikron.SharedKernel.Infrastructure.Commands
{
    /// <summary>
    /// A base class for handlers command and queries without dependencies on other services like IDispatcher or IMapper
    /// </summary>
    /// <typeparam name="TRequest">A request type</typeparam>
    /// <typeparam name="TResponse">A response type</typeparam>
    public abstract class BaseHandlerLight<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {

        protected BaseHandlerLight()
        {
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}