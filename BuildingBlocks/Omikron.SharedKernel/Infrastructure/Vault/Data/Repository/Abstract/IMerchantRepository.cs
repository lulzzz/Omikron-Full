using Omikron.SharedKernel.Infrastructure.Data.Repository;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels.Analytics;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract
{
	public interface IMerchantRepository : IRepositoryBase<Merchant>
	{
		Task<IEnumerable<Merchant>> GetAll(CancellationToken cancellationToken = default);
		Task<IEnumerable<Merchant>> GetMerchantsByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default);
		Task<MerchantContainerViewModel> GetMerchantData(CustomerId customerId, DateRange dateFilter, CancellationToken cancellationToken);
		Task<IEnumerable<MerchantViewModel>> GetFilteredMerchants(CustomerId customerId, DateRange dateFilter, int page, int pageSize, IEnumerable<AccountType> assetLiabilityTypes, IEnumerable<Guid> vaultEntries, IEnumerable<string> categories, CancellationToken cancellationToken);
	}
}
