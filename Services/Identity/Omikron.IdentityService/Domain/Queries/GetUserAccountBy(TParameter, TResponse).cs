using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.IdentityService.Domain.Queries
{
    public class GetUserAccountBy
    {
        public abstract class Query<TParameter, TResponse> : BaseCommand.Action<ApiResult<TResponse>>
        {
            protected Query()
            {
            }

            protected Query(TParameter parameter)
            {
                Parameter = parameter;
            }

            public TParameter Parameter { get; set; }
        }
    }
}