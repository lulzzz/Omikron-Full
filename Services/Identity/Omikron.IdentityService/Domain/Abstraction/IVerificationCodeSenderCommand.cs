using MediatR;

namespace Omikron.IdentityService.Domain.Abstraction
{
    public interface IVerificationCodeSenderCommand<TResponse> : IRequest<TResponse>
    {
    }
}
