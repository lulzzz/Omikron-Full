using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.VaultService.Domain.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Configuration;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.SharedKernel.Infrastructure.Vault.Services;
using Omikron.SharedKernel.Infrastructure.Logging.Context;
using Omikron.SharedKernel.Infrastructure.Vault.Extensions;

namespace Omikron.VaultService.Domain.Handlers
{
	public class GetVaultQueryHandler : BaseHandlerLight<GetVault.Query, ApiResult<VaultViewModel>>
    {
        private readonly IVaultItemRepository _vaultItemRepository;
        private readonly IAccountService _accountService;
        private readonly BudProviderIcons _budProviderIcons;
		private readonly LoggerContext _logger;

		public GetVaultQueryHandler(IVaultItemRepository vaultItemRepository, IAccountService accountService, BudProviderIcons budProviderIcons, LoggerContext logger)
        {
            _vaultItemRepository = vaultItemRepository;
            _accountService = accountService;
            _budProviderIcons = budProviderIcons;
			_logger = logger;
		}

        public override async Task<ApiResult<VaultViewModel>> Handle(GetVault.Query request, CancellationToken cancellationToken)
        {
            var userVault = await _vaultItemRepository.GetOwnerVaultItems(CustomerId.Parse(request.UserId), cancellationToken);

            var result = new VaultViewModel
            {
                Accounts = GetUserAccounts(userVault),
                Assets = GetUserAssets(userVault)
            };

            return ApiResult<VaultViewModel>.Success().WithData(result);
        }

        private IEnumerable<AssetGruopViewModel> GetUserAssets(IEnumerable<VaultItem> userVault)
        {
            return userVault.Where(x => x.ItemType != VaultItemType.Account)
                            .OrderBy(a => a.ItemType.Id)
                            .GroupBy(a => a.ItemType)
                            .Select(g => AggregateAssets(g));
        }

        private AssetGruopViewModel AggregateAssets(IGrouping<VaultItemType, VaultItem> g)
        {
            return new AssetGruopViewModel
            {
                AssetTypeName = g.Key.ToString().ToAccountGroupDisplayName(),
                AssetType = g.Key.ToString(),
                Count = g.Count(),
                Total = _accountService.CalculateTotalBalance(g),
                Assets = g.Select(a => CreateAssetViewModel(a))
            };
        }

        private static AssetViewModel CreateAssetViewModel(VaultItem a)
        {
            return new AssetViewModel
            {
                HostId = a.HostId,
                ImageUrl = a.ImageUrl,
                Name = a.Name,
                Value = a.Value
            };
        }

        private IEnumerable<AccountGroupViewModel> GetUserAccounts(IEnumerable<VaultItem> userVault)
        {
            return userVault.Where(x => x.ItemType == VaultItemType.Account)
                            .OrderBy(a => a.AccountType.Id)
                            .GroupBy(a => a.AccountType)
                            .Select(g => AggregateAccounts(g));
        }

        private AccountGroupViewModel AggregateAccounts(IGrouping<AccountType, VaultItem> g)
        {
            return new AccountGroupViewModel
            {
                AccountTypes = g.Key.ToString().ToAccountGroupDisplayName(),
                Count = g.Count(),
                Total = _accountService.CalculateTotalBalance(g),
                Accounts = g.Select(a => CreateAccountViewModel(a))
            };
        }

        private AccountViewModel CreateAccountViewModel(VaultItem a)
        {
            var providerColour = _budProviderIcons.DefaultBackground;
            var imageUrl = "";

            if (!string.IsNullOrWhiteSpace(a.AccountProvider) && _budProviderIcons.Providers.ContainsKey(a.AccountProvider))
            {
                var providerIcon = _budProviderIcons.Providers[a.AccountProvider];
                providerColour = providerIcon.Colour;
                imageUrl = providerIcon.Icon;
            }
            else
			{
                _logger.UsageLogger.Error($"Missing logo and background for {a.AccountProvider}");
			}

            return new AccountViewModel
            {
                HostId = a.HostId,
                Provider = a.AccountProvider,
                IdentificationNumber = a.AccountIdentificationNumber?.ToString(),
                Name = a.Name,
                Value = a.CreditDebitIndicator == CreditDebitIndicator.Credit ? a.Value : a.Value * (-1),
                AuthorizationStatus = a.AccountExpiryDate.HasValue ? AuthorizationStatus.Parse(a.AccountExpiryDate.Value) : AuthorizationStatus.Valid.ToString(),
                ImageUrl = imageUrl,
                AccountSource = a.AccountSource,
                AccountType = a.AccountType.ToString().ToAccountGroupDisplayName(),
                AssetType = AssetType.Parse(a.AccountType),
                ProviderColour = providerColour
            };
        }
    }
}