using AutoMapper;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class GetAccountTransactionsQueryHandler : BaseHandlerLight<GetAccountTransactions.Query, ApiResult<IEnumerable<TransactionViewModelContainer>>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetAccountTransactionsQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        //TODO: This needs to be improved once there is more time to do it
        public override async Task<ApiResult<IEnumerable<TransactionViewModelContainer>>> Handle(GetAccountTransactions.Query request, CancellationToken cancellationToken)
        {
            var transactions =  await _transactionRepository.SearchTransactionsByAccountId(request.AccountId, request.SearchTerm, cancellationToken);

            var result = transactions.OrderByDescending(t => t.Date)
                                     .GroupBy(t => t.Date.Date)
                                     .Select(g => new TransactionViewModelContainer()
                                     {
                                         Date = g.Key.ToString("dddd, dd MMMM yyyy"),
                                         Transactions = _mapper.Map<IEnumerable<TransactionViewModel>>(g.AsEnumerable())
                                     })
                                     .Skip((request.Page - 1) * request.PageSize)
									 .Take(request.PageSize);

			return ApiResult<IEnumerable<TransactionViewModelContainer>>.Success().WithData(result);
        }
    }
}
