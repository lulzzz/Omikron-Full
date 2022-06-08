using CSharpFunctionalExtensions;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Services;
using Omikron.SharedKernel.Utils;
using Omikron.Sync.Bud.Models;
using Omikron.Sync.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.Sync.Bud.Channels.Accounts
{
	public class BudAccountsSyncSource : ISyncSource<User, CustomerAccountsData>
	{
		private readonly IBudApiService _budApiService;

		public BudAccountsSyncSource(IBudApiService budApiService)
		{
			_budApiService = budApiService;
		}

		public async Task<Maybe<SyncSourcePayload<CustomerAccountsData>>> FetchAsync(User entity, CancellationToken cancellationToken)
		{
			var accounts = await _budApiService.GetFromApi<BudBaseResponse<IEnumerable<BudListAccountsResponse>, ListAccountsMetadata>>(BudApiEndpoints.ListAccounts, entity.BudCustomerId, entity.BudCustomerSecret, cancellationToken);
			var consents = await _budApiService.GetFromApi<BudBaseResponse<IEnumerable<BudListCustomerConsentsResponse>>>(BudApiEndpoints.ListCustomerConsents, entity.BudCustomerId, entity.BudCustomerSecret, cancellationToken);

			var customerAccountsData = new CustomerAccountsData(accounts.Data, consents.Data);

			var payload = new SyncSourcePayload<CustomerAccountsData>(customerAccountsData);

			return Maybe<SyncSourcePayload<CustomerAccountsData>>.From(payload);
		}
	}
}
