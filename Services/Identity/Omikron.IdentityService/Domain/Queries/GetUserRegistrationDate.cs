using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;

namespace Omikron.IdentityService.Domain.Queries
{
	public class GetUserRegistrationDate
    {
        public class Query : BaseCommand.Action<ApiResult<UserRegistrationDateViewModel>>
        {
			public string Username { get; set; }

			public Query(string username)
			{
				Username = username;
			}
		}
    }
}
