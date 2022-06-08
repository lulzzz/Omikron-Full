using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Api;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Security;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Commands;
using Omikron.VaultService.Domain.Commands.ManualAccounts;
using Omikron.VaultService.Domain.Queries;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Omikron.VaultService.Controllers
{
	[AuthorizeByTenantAndCredentials]
	[Route(template: "api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion(version: "1.0")]
	public class VaultController : BaseMultiTenantController
	{
		private readonly IServiceProvider _serviceProvider;
		private IValidator<T> Validator<T>() => _serviceProvider.GetService<IValidator<T>>();

		public VaultController(IDispatcher dispatcher, IServiceProvider serviceProvider) : base(dispatcher: dispatcher, serviceProvider: serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		[HttpGet("{userId}/get-accounts"), SwaggerOperation(OperationId = "GetAccounts.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> GetAccounts(Guid userId)
		{
			var result = await Dispatcher.DispatchAsync(new GetAccounts.Query(userId));
			return result.ToActionResult();
		}

		[HttpGet("{userId}/get-summary"), SwaggerOperation(OperationId = "GetSummary.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> GetSummary(Guid userId)
		{
			var result = await Dispatcher.DispatchAsync(new GetSummary.Query(userId));
			return result.ToActionResult();
		}

		[HttpGet("{userId}/get-vault"), SwaggerOperation(OperationId = "GetVault.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> GeVault(Guid userId)
		{
			var result = await Dispatcher.DispatchAsync(new GetVault.Query(userId));
			return result.ToActionResult();
		}

		[HttpGet("{userId}/get-last-refresh"), SwaggerOperation(OperationId = "GetLastRefresh.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> GetLastRefresh(Guid userId)
		{
			var result = await Dispatcher.DispatchAsync(new GetLastRefresh.Query(userId));
			return result.ToActionResult();
		}

		[HttpGet("{accountId}/account-details"), SwaggerOperation(OperationId = "GetAccountDetails.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> GetAccountDetails(Guid accountId)
		{
			var result = await Dispatcher.DispatchAsync(new GetAccountDetails.Query(accountId));
			return result.ToActionResult();
		}

		[HttpGet("{accountId}/transactions"), SwaggerOperation(OperationId = "GetAccountTransactions.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> GetAccountTransactions(Guid accountId, [FromQuery] int page, [FromQuery] string search)
		{
			var result = await Dispatcher.DispatchAsync(new GetAccountTransactions.Query(accountId, page, search));
			return result.ToActionResult();
		}

		[HttpDelete("revoke-consent/{providerName}"), SwaggerOperation(OperationId = "RevokeConsent.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> RevokeConsent([FromRoute] string providerName)
		{
			var result = await Dispatcher.DispatchAsync(new RevokeConsent.Command(Username, providerName));
			return result.ToActionResult();
		}

		[HttpPut("{userId}/refresh"), SwaggerOperation(OperationId = "Refresh.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> Refresh(Guid userId)
		{
			var result = await Dispatcher.DispatchAsync(new Refresh.Command(userId));
			return result.ToActionResult();
		}

		[HttpDelete("{userId}/related-data"), SwaggerOperation(OperationId = "DeleteUserRelatedData.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> DeleteUserRelatedData(Guid userId)
		{
			var result = await Dispatcher.DispatchAsync(new DeleteUserRelatedData.Command(userId));
			return result.ToActionResult();
		}

		[HttpPost("personal-item"), SwaggerOperation(OperationId = "AddPersonalItem.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> AddPersonalItem(AddPersonalItem.Command command)
		{
			var result = await Dispatcher.DispatchAsync(command);
			return result.ToActionResult();
		}

		[HttpPost("investment"), SwaggerOperation(OperationId = "AddInvestment.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> AddInvestment(AddInvestment.Command command)
		{
			var result = await Dispatcher.DispatchAsync(command);
			return result.ToActionResult();
		}

		[HttpPost("vault-item-photo"), SwaggerOperation(OperationId = "AddVaultItemPhoto.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> AddVaultItemPhoto(IFormFile file)
		{
			var command = new AddVaultItemPhoto.Command(file);
			var validationResult = Validator<AddVaultItemPhoto.Command>().Validate(command);
			if (!validationResult.IsValid)
			{
				var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray();
				return ApiResult.BadRequest(errors).ToActionResult();
			}

			var result = await Dispatcher.DispatchAsync(command);
			return result.ToActionResult();
		}

		[HttpGet("{userId}/available-loans"), SwaggerOperation(OperationId = "GetAvailableLoans.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> GetAvailableLoans(Guid userId, [FromQuery] string search)
		{
			var result = await Dispatcher.DispatchAsync(new GetAvailableLoans.Query(userId, search));
			return result.ToActionResult();
		}
		[HttpDelete("vault-item-photo/{blobName}"), SwaggerOperation(OperationId = "DeleteVaultItemPhoto.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		[SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
		public async Task<IActionResult> DeleteVaultItemPhoto(string blobName)
		{
			var command = new DeleteVaultItemPhoto.Command(blobName);
			return await SendTenantCommandAsync(command);
		}

		[HttpGet("{userId}/category-transactions"), SwaggerOperation(OperationId = "GetCategoryTransactions.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		public async Task<IActionResult> GetCategoryTransactions([FromRoute] Guid userId, [FromQuery] GetCategoryTransactions.Query query)
		{
			query.UserId = userId;
			var result = await Dispatcher.DispatchAsync(query);
			return result.ToActionResult();
		}

		[HttpPost("manual-account"), SwaggerOperation(OperationId = "AddManualAccount.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> AddManualAccount(AddManualAccount.Command command)
		{
			var result = await Dispatcher.DispatchAsync(command);
			return result.ToActionResult();
		}

		[HttpGet("{userId}/top-merchants"), SwaggerOperation(OperationId = "GetMerchants.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> GetMerchants([FromRoute] Guid userId, [FromQuery] GetMerchants.Query query)
		{
			query.UserId = userId;
			var result = await Dispatcher.DispatchAsync(query);
			return result.ToActionResult();
		}

		[AllowAnonymous]
		[HttpGet("vehicle-value"), SwaggerOperation(OperationId = "GetVehicleValue.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> GetVehicleValue([FromQuery] GetVehicleValue.Query query)
		{
			var result = await Dispatcher.DispatchAsync(query);
			return result.ToActionResult();
		}

		[HttpPost("vehicle"), SwaggerOperation(OperationId = "AddVehicle.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> AddVehicle(AddVehicle.Command command)
		{
			var result = await Dispatcher.DispatchAsync(command);
			return result.ToActionResult();
		}

		[HttpGet("vehicle"), SwaggerOperation(OperationId = "GetVehicleDetails.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
		public async Task<IActionResult> GetVehicleDetails([FromQuery] GetVehicleDetails.Query query)
		{
			var result = await Dispatcher.DispatchAsync(query);

			return result.ToActionResult();
		}

		[HttpPut("vehicle"), SwaggerOperation(OperationId = "UpdateVehicle.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicle.Command command)
		{
			var result = await Dispatcher.DispatchAsync(command);

			return result.ToActionResult();
		}

		[HttpGet("manual-account-details/{accountId}/{itemType}/{financeId?}"), SwaggerOperation("GetManualAccountDetails.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		[SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
		public async Task<IActionResult> ManualAccountDetails([Required] Guid accountId, [Required] string itemType, Guid financeId = default)
		{
			var query = new GetManualAccountDetails.Query() { AccountId = accountId, ItemType = AssetType.Parse(itemType), FinanceId = financeId };
			var result = await Dispatcher.DispatchAsync(query);
			return result.ToActionResult();
		}

		[HttpDelete("remove-manual-account/{accountId}/{accountType}/{isArchived}"), SwaggerOperation("RemoveManualAccount.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		[SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
		public async Task<IActionResult> RemoveManualAccount([Required] Guid accountId, [Required] string accountType, [Required] bool isArchived)
		{
			var command = new RemoveManualAccount.Command { AccountId = accountId, AccountType = AssetType.Parse(accountType), IsArchived = isArchived };
			var result = await Dispatcher.DispatchAsync(command);
			return result.ToActionResult();
		}

		[AllowAnonymous]
		[HttpGet("property-value"), SwaggerOperation(OperationId = "GetPropertyValue.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp204)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> GetPropertyValue([FromQuery] GetPropertyValue.Query query)
		{
			var result = await Dispatcher.DispatchAsync(query);

			return result.ToActionResult();
		}

		[HttpPost("property"), SwaggerOperation(OperationId = "AddProperty.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(401, SwaggerConstants.ResponseHttp401)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> AddProperty(AddProperty.Command request)
		{
			var result = await Dispatcher.DispatchAsync(request);
			return result.ToActionResult();
		}

		[HttpGet("{userId}/min-analytics-date"), SwaggerOperation(OperationId = "GetMinimumAnalyticsDate.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> GetMinimumAnalyticsDate([FromRoute] Guid userId)
		{
			var result = await Dispatcher.DispatchAsync(new GetMinimumAnalyticsDate.Query(userId));
			return result.ToActionResult();
		}

		[HttpGet("{userId}/net-positions-chart-data"), SwaggerOperation(OperationId = "GetNetPositionsChartData.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> GetNetPositionsChartData([FromRoute] Guid userId, [FromQuery] GetNetPositionsChartData.Query query)
		{
			query.UserId = userId;
			var result = await Dispatcher.DispatchAsync(query);
			return result.ToActionResult();
		}

		[HttpGet("{userId}/dashboard-chart-data"), SwaggerOperation(OperationId = "GetDashboardChartData.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> GetDashboardChartData([FromRoute] Guid userId, [FromQuery] GetDashboardChartData.Query query)
		{
			query.UserId = userId;
			var result = await Dispatcher.DispatchAsync(query);
			return result.ToActionResult();
		}

		[HttpGet("{userId}/vault-entry-list"), SwaggerOperation(OperationId = "GetVaultEntryList.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> GetVaultEntryList([FromRoute] Guid userId, [FromQuery] IEnumerable<string> assetLiabilityTypes)
		{
			var result = await Dispatcher.DispatchAsync(new GetVaultEntryList.Query(userId, assetLiabilityTypes));
			return result.ToActionResult();
		}

		[HttpGet("property"), SwaggerOperation(OperationId = "GetPropertyDetails.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
		public async Task<IActionResult> GetPropertyDetails([FromQuery] GetPropertyDetails.Query query)
		{
			var result = await Dispatcher.DispatchAsync(query);

			return result.ToActionResult();
		}

		[HttpPut("property"), SwaggerOperation(OperationId = "UpdateProperty.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> UpdateProperty([FromBody] UpdateProperty.Command command)
		{
			var result = await Dispatcher.DispatchAsync(command);

			return result.ToActionResult();
		}

		[HttpGet("personal-item"), SwaggerOperation(OperationId = "GetPersonalItemDetails.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
		public async Task<IActionResult> GetPersonalItemDetails([FromQuery] GetPersonalItemDetails.Query query)
		{
			var result = await Dispatcher.DispatchAsync(query);
			return result.ToActionResult();
		}

		[HttpPut("personal-item"), SwaggerOperation(OperationId = "UpdatePersonalItem.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> UpdatePersonalItem([FromBody] UpdatePersonalItem.Command command)
		{
			var result = await Dispatcher.DispatchAsync(command);
			return result.ToActionResult();
		}

		[HttpGet("investment"), SwaggerOperation(OperationId = "GetInvestmentDetails.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
		public async Task<IActionResult> GeInvestmentDetails([FromQuery] GetInvestmentDetails.Query query)
		{
			var result = await Dispatcher.DispatchAsync(query);
			return result.ToActionResult();
		}

		[HttpPut("investment"), SwaggerOperation(OperationId = "UpdateInvestment.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> UpdateInvestment([FromBody] UpdateInvestment.Command command)
		{
			var result = await Dispatcher.DispatchAsync(command);
			return result.ToActionResult();
		}

		[HttpGet("manual-account"), SwaggerOperation(OperationId = "GetManualAccount.Query")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(404, SwaggerConstants.ResponseHttp404)]
		public async Task<IActionResult> GetManualAccount([FromQuery] GetManualAccount.Query query)
		{
			var result = await Dispatcher.DispatchAsync(query);
			return result.ToActionResult();
		}

		[HttpPut("manual-account"), SwaggerOperation(OperationId = "UpdateManualAccount.Command")]
		[SwaggerResponse(200, SwaggerConstants.ResponseHttp200)]
		[SwaggerResponse(400, SwaggerConstants.ResponseHttp400)]
		public async Task<IActionResult> UpdateManualAccount([FromBody] UpdateManualAccount.Command command)
		{
			var result = await Dispatcher.DispatchAsync(command);
			return result.ToActionResult();
		}
	}
}