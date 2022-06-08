using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
	public class GetUserRegistrationDateQueryHandler : BaseHandlerLight<GetUserRegistrationDate.Query, ApiResult<UserRegistrationDateViewModel>>
	{
		private readonly IdentityUserManager _userManager;

		public GetUserRegistrationDateQueryHandler(IdentityUserManager userManager)
		{
			_userManager = userManager;
		}

		public async override Task<ApiResult<UserRegistrationDateViewModel>> Handle(GetUserRegistrationDate.Query request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByNameAsync(request.Username);

			var result = new UserRegistrationDateViewModel(user.RegistrationDate.Day, user.RegistrationDate.Month, user.RegistrationDate.Year);

			return ApiResult<UserRegistrationDateViewModel>.Success().WithData(result);
		}
	}
}