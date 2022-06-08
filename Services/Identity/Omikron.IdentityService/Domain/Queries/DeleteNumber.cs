using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.IdentityService.Domain.Queries
{
    public class DeleteNumber
    {
        public class Query : BaseCommand.Action<ApiResult>
        {
            public string Number { get; set; }

            public Query(string number)
            {
                Number = number;
            }
        }
    }
}
