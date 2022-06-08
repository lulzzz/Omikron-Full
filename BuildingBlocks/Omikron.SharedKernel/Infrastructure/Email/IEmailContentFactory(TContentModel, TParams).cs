using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Email
{
    public interface IEmailContentFactory<out TContentModel, in TParams> : IFactory<TContentModel, TParams>
    {
    }
}