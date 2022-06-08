using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Messaging;

namespace Omikron.IdentityService.Domain.Commands
{
	public class CreateBudCustomer
    {
        public class Command : TenantCommand<ApiResult>
        {
			public User User { get; set; }

			public Command(User user)
			{
				User = user;
			}
		}
    }
}
