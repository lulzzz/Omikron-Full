using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.VaultService.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;

namespace Omikron.VaultService.Domain.Handlers
{
    public class DeleteUserRelatedDataCommandHandler : BaseHandlerLight<DeleteUserRelatedData.Command, ApiResult>
    {

        private readonly IVehicleRepository _vehicleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IVaultItemRepository _vaultItemRepository;
		private readonly IPersonalItemRepository _personalItemRepository;
		private readonly IPropertyRepository _propertyRepository;
        private readonly IRefreshHistoryRepository _refreshHistoryRepository;
        private readonly IInvestmentRepository _investmentRepository;

        public DeleteUserRelatedDataCommandHandler(IRefreshHistoryRepository refreshHistoryRepository,
            IAccountRepository accountRepository, IPropertyRepository propertyRepository,
            IVehicleRepository vehicleRepository, IVaultItemRepository vaultItemRepository,
            IPersonalItemRepository personalItemRepository, IInvestmentRepository investmentRepository)
        {
            _vehicleRepository = vehicleRepository;
            _accountRepository = accountRepository;
            _vaultItemRepository = vaultItemRepository;
			_personalItemRepository = personalItemRepository;
			_propertyRepository = propertyRepository;
            _refreshHistoryRepository = refreshHistoryRepository;
            _investmentRepository = investmentRepository;
        }

        public override async Task<ApiResult> Handle(DeleteUserRelatedData.Command command, CancellationToken cancellationToken)
        {
            var userId = CustomerId.Parse(command.UserId);

            await _vehicleRepository.DeleteVehiclesByUserId(userId, cancellationToken);
            await _propertyRepository.DeletePropertiesByOwnerId(userId, cancellationToken);
            await _vaultItemRepository.DeleteVaultItemsByUserId(userId, cancellationToken);
            await _accountRepository.DeleteAccountsByOwnerId(userId, cancellationToken);
            await _personalItemRepository.DeletePersonalItemsByUserId(userId, cancellationToken);
            await _refreshHistoryRepository.DeleteRefreshHistoryByUserId(command.UserId, cancellationToken);
            await _investmentRepository.DeleteInvestmentsByOwnerId(userId, cancellationToken);

            await _accountRepository.SaveChangesAsync(cancellationToken);

            return ApiResult.Success();
        }
    }
}