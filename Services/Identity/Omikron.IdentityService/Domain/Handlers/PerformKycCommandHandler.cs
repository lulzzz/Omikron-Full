using AutoMapper;
using Omikron.IdentityService.Domain.Commands;
using Omikron.IdentityService.Domain.Events;
using Omikron.IdentityService.Domain.Services;
using Omikron.IdentityService.Infrastructure.Data.Model;
using Omikron.IdentityService.Infrastructure.IdentityServer;
using Omikron.IdentityService.Models;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Utils;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Omikron.TenantService.Domain.Services;

namespace Omikron.IdentityService.Domain.Handlers
{
	public class PerformKycCommandHandler : BaseHandler<PerformKyc.Command, ApiResult>
	{
		private readonly IdentityUserManager _userManager;
		private readonly IBudApiService _budApiService;
		private readonly IMapper _mapper;
		private readonly ILocationLookupService _locationLookupService;
        private readonly IUserAccountService _userAccountService;

		public PerformKycCommandHandler(IDispatcher dispatcher, IdentityUserManager userManager, IBudApiService budApiService, IMapper mapper, ILocationLookupService locationLookupService, IUserAccountService userAccountService) : base(dispatcher)
		{
			_userManager = userManager;
			_budApiService = budApiService;
			_mapper = mapper;
			_locationLookupService = locationLookupService;
            _userAccountService = userAccountService;
		}

		public override async Task<ApiResult> Handle(PerformKyc.Command command, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByNameAsync(command.UserName);
			if (user == null)
			{
				return ApiResult.NotFound($"The user '{command.UserName}' does not exist.");
			}

			if (user.BudCustomerId != null && user.BudCustomerSecret != null)
			{
				return ApiResult.BadRequest($"User already verified");
			}

			var kycRequest = await FactoryKycRequest(command, user, cancellationToken);
			var kycCheckGuid = await _budApiService.PostToApi<BudBaseResponse<BudInitiateKycResponse>, InitiatePersonCheckRequest>(BudApiEndpoints.InitiatePersonCheck, kycRequest, cancellationToken: cancellationToken);

			var retrieveKycCheckEndpoint = $"{BudApiEndpoints.RetrieveKYCCheck}/{kycCheckGuid.Data.CheckUuid}";
			var kycCheckResponse = await GetKycStatus(retrieveKycCheckEndpoint, cancellationToken);

			if (kycCheckResponse.Data.Rag != Constants.KycApproved)
			{
				return ApiResult.BadRequest("We couldn't verify your identity. Please check the details you have provided and try again");
			}

			_mapper.Map(command, user);

			var createBudCustomerResult = await Dispatcher.DispatchAsync(new CreateBudCustomer.Command(user), cancellationToken);

			if (!createBudCustomerResult.IsSuccess)
			{
				return ApiResult.BadRequest(createBudCustomerResult.Errors);
			}

            await _userAccountService.SendNewUserEmailAsync(user);
			await Dispatcher.DispatchAsync(new UserUpdatedEvent(user.Id), cancellationToken);
			return ApiResult.Success();
		}

		private async Task<BudBaseResponse<BudRetrieveKYCCheckResponse>> GetKycStatus(string retrieveKycCheckEndpoint, CancellationToken cancellationToken)
		{
			var kycCheckResponse = await _budApiService.GetFromApi<BudBaseResponse<BudRetrieveKYCCheckResponse>>(retrieveKycCheckEndpoint, cancellationToken: cancellationToken);
			if (kycCheckResponse.Data.Status == Constants.KycCompleted)
			{
				return kycCheckResponse;
			}

			var wait = 0;
			for (var i = 1; i <= 5; i++)
			{
				kycCheckResponse = await _budApiService.GetFromApi<BudBaseResponse<BudRetrieveKYCCheckResponse>>(retrieveKycCheckEndpoint, cancellationToken: cancellationToken);
				if (kycCheckResponse.Data.Status == Constants.KycCompleted)
				{
					break;
				}

				wait += SharedKernel.Constants.BackoffFactorInMilliseconds;

				await Task.Delay(wait, cancellationToken);
			}

			return kycCheckResponse;
		}

		private async Task<InitiatePersonCheckRequest> FactoryKycRequest(PerformKyc.Command command, User user, CancellationToken cancellationToken = default)
		{
			var request = new InitiatePersonCheckRequest()
			{
				Person = new()
				{
					FirstName = command.FirstName,
					LastName = command.LastName,
					DateOfBirth = command.DateOfBirth.ToString("yyyy-MM-dd"),
					EmailAddress = new()
					{
						user.Email
					}
				},
				Header = new()
				{
					ProfileUuid = Constants.BudProfileUuid,
					TransactionReference = Constants.BudTransactionReference
				}
			};

			if (command.IsManualAddress)
			{
				var addressDetails = command.Address.Split(",");

				request.Person.CurrentAddress = new()
				{
					Country = SharedKernel.Constants.EnglandISOCountryCode,  // "Hardcoding" this since all users will be from England
					Postcode = command.Postcode,
					LineThree = addressDetails.Length == 4 ? addressDetails[2] : "",
					Town = addressDetails.Length == 4 ? addressDetails[3] : addressDetails[2],
					NameNumber = addressDetails[0],
					Street = addressDetails[1]
				};
			}
			else
			{
				var userAddress = await GetUserAddressDetails(command.Postcode, command.Address, cancellationToken);

				request.Person.CurrentAddress = new()
				{
					Country = SharedKernel.Constants.EnglandISOCountryCode,  // "Hardcoding" this since all users will be from England
					County = userAddress?.County ?? "",
					LineFour = userAddress?.Line4 ?? "",
					LineThree = userAddress?.Line3 ?? "",
					Postcode = command.Postcode,
					Town = userAddress?.TownOrCity ?? "",
					NameNumber = userAddress?.BuildingNumber ?? "",
					Street = userAddress?.Line1 ?? ""
				};
			}

			return request;
		}

		private async Task<PostcodeAddress> GetUserAddressDetails(string postcode, string userAddress, CancellationToken cancellationToken)
		{
			var postcodeAddresses = await _locationLookupService.GetLocationsByPostcodeExpanded(postcode, cancellationToken);

			return postcodeAddresses.Addresses?.FirstOrDefault(a => string.Join(",", a.FormattedAddress).ReplaceByEmpty(",", " ") == userAddress.ReplaceByEmpty(",", " "));
		}
	}
}
