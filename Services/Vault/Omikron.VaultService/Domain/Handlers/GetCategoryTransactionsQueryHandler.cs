using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.VaultService.Domain.Queries;
using Omikron.VaultService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class GetCategoryTransactionsQueryHandler : BaseHandlerLight<GetCategoryTransactions.Query, ApiResult<CategoryTransactionsViewModel>>
	{
		private readonly ITransactionRepository _transactionRepository;

		public GetCategoryTransactionsQueryHandler(ITransactionRepository transactionRepository)
		{
			_transactionRepository = transactionRepository;
		}

		public override async Task<ApiResult<CategoryTransactionsViewModel>> Handle(GetCategoryTransactions.Query request, CancellationToken cancellationToken)
		{
			var dateFilter = CreateDateFilter(request);

			var accountTypes = AccountType.EnumerateTypesAsDisplayNames();
			var assetLiabilityTypes = request.AssetLiabilityTypes?.Where(x => accountTypes.Contains(x)).Select(x => AccountType.Parse(x)) ?? new List<AccountType>();
			var vaultEntries = request.VaultEntries ?? new List<Guid>();
			var categories = request.Categories ?? new List<string>();
			var data = await _transactionRepository.GetFilteredCategories(CustomerId.Parse(request.UserId), dateFilter, assetLiabilityTypes, vaultEntries, categories, cancellationToken);

			var result = CreateResultData(data);

			return ApiResult<CategoryTransactionsViewModel>.Success().WithData(result);
		}

		private static CategoryTransactionsViewModel CreateResultData(IEnumerable<CategoryData> data)
		{
			var result = new CategoryTransactionsViewModel();

			foreach (var item in data)
			{

				if (item.DebitAmount > 0)
				{
					result.TotalExpenditure -= item.DebitAmount;
					result.TotalNumberOfExpenditureTransactions += item.NumberOfDebitTransactions;

					result.Expenditure.Add(new()
					{
						CategoryName = item.CategoryName,
						Amount = item.DebitAmount * (-1),
						NumberOfTransactions = item.NumberOfDebitTransactions,
						Icon = $"{item.CategoryName.ToLower().Replace(" ", "_")}.svg"
					});
				}

				if (item.CreditAmount > 0)
				{
				
					result.TotalIncome += item.CreditAmount;
					result.TotalNumberOfIncomeTransactions += item.NumberOfCreditTransactions;

					result.Income.Add(new()
					{
						CategoryName = item.CategoryName,
						Amount = item.CreditAmount,
						NumberOfTransactions = item.NumberOfCreditTransactions,
						Icon = $"{item.CategoryName.ToLower().Replace(" ", "_")}.svg"
					});
				}
			}

			return result;
		}

		private static DateRange CreateDateFilter(GetCategoryTransactions.Query request)
		{
			return request.MonthIndex.HasValue ? new DateRange(new DateTime(request.Year, request.MonthIndex.Value, 1),
															   new DateTime(request.Year, request.MonthIndex.Value, DateTime.DaysInMonth(request.Year, request.MonthIndex.Value)))
											   : new DateRange(new DateTime(request.Year, 1, 1),
															   new DateTime(request.Year, SharedKernel.Constants.NumberOfMonthsInYear, DateTime.DaysInMonth(request.Year, SharedKernel.Constants.NumberOfMonthsInYear)));
		}
	}
}
