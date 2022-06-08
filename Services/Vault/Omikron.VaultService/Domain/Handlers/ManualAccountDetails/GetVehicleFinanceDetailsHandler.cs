using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries.ManualAccountDetails;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class GetVehicleFinanceDetailsHandler : BaseHandlerWithAutoMapper<GetVehicleFinanceDetails.Query, ManualAccountDetailsViewModel>
    {
        private readonly IAccountRepository _accountRepository;

        public GetVehicleFinanceDetailsHandler(IAccountRepository accountRepository, IMapper mapper, IDispatcher dispatcher) : base(mapper, dispatcher)
        {
            _accountRepository = accountRepository;
        }

        public override async Task<ManualAccountDetailsViewModel> Handle(GetVehicleFinanceDetails.Query request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetVehicleFinanceAccount(request.FinanceId, request.AccountId, cancellationToken);
            return account;
        }
    }
}