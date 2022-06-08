using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Utils;
using Omikron.Sync.Model;

namespace Omikron.Sync.Bud.Channels.Transactions
{
    public sealed class BudTransactionSyncSource : ISyncSource<User, IEnumerable<BudListTransactionsResponse>>
    {
        private readonly IBudApiService _budApiService;

        public BudTransactionSyncSource(IBudApiService budApiService)
        {
            _budApiService = budApiService;
        }

        public async Task<Maybe<SyncSourcePayload<IEnumerable<BudListTransactionsResponse>>>> FetchAsync(User user, CancellationToken cancellationToken)
        {
            var dateFilter = new DateRange(Clock.GetTime().AddYears(-1), Clock.GetTime());
            var headers = new Dictionary<string, string>
            {
                { "X-From", dateFilter.From.ToString("yyyy-MM-dd") },
                { "X-To", dateFilter.To.ToString("yyyy-MM-dd") }
            };

            var transactions = await _budApiService.GetFromApi<BudBaseResponse<IEnumerable<BudListTransactionsResponse>>>(endpoint: BudApiEndpoints.ListTransactions, headers, customerId: user.BudCustomerId, customerSecret: user.BudCustomerSecret, cancellationToken: cancellationToken);
            var payload = new SyncSourcePayload<IEnumerable<BudListTransactionsResponse>>(value: transactions.Data);
            return Maybe<SyncSourcePayload<IEnumerable<BudListTransactionsResponse>>>.From(obj: payload);
        }
    }
}