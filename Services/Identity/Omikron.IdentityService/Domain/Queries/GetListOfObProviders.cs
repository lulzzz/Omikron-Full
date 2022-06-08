using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Collections.Generic;
using System.Linq;

namespace Omikron.IdentityService.Domain.Queries
{
    public class GetListOfObProviders
    {
        public class Query : BaseCommand.Action<ApiResult<Dictionary<char, IGrouping<char, ObProviderViewModel>>>>
        {
            public string SearchTerm { get; set; }

            public Query()
            {
            }

            public Query(string searchTerm)
            {
                SearchTerm = searchTerm;
            }
        }
    }
}
