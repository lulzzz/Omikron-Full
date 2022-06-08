using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.IdentityService.Domain.Queries
{
    public class DeleteUser
    {
        public class Query : BaseCommand.Action<ApiResult>
        {
            public string User { get; set; }

            public Query(string user)
            {
                User = user;
            }
        }
    }
}
