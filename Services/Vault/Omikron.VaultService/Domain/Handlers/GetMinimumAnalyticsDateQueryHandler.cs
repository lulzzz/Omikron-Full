using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Utils;
using Omikron.VaultService.Domain.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class GetMinimumAnalyticsDateQueryHandler : BaseHandlerLight<GetMinimumAnalyticsDate.Query, ApiResult<DateTime>>
	{
		private readonly ITransactionRepository _transactionRepository;

		public GetMinimumAnalyticsDateQueryHandler(ITransactionRepository transactionRepository)
		{
			_transactionRepository = transactionRepository;
		}

		public override async Task<ApiResult<DateTime>> Handle(GetMinimumAnalyticsDate.Query request, CancellationToken cancellationToken)
		{
			var firstTransaction = await _transactionRepository.GetFirstTransactionByDate(CustomerId.Parse(request.UserId), cancellationToken);
			var result = firstTransaction != null ? firstTransaction.Date : Clock.GetTime();
			return ApiResult<DateTime>.Success().WithData(result);
		}
	}
}
