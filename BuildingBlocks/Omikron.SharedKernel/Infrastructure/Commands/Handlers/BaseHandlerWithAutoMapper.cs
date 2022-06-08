using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Omikron.SharedKernel.Infrastructure.Commands
{
    /// <summary>
    /// A base class for handlers command and queries with IDispatcher and IMapper dependencies. Use it when you need both Dispatcher and Automapper.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class BaseHandlerWithAutoMapper<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IDispatcher Dispatcher;
        protected readonly IMapper Mapper;

        protected BaseHandlerWithAutoMapper(IMapper mapper, IDispatcher dispatcher)
        {
            Mapper = mapper;
            Dispatcher = dispatcher;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}