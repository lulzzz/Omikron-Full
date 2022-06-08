using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries.ManualAccountDetails;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class GetMortgageDetailsHandler : BaseHandlerWithAutoMapper<GetMortgageDetails.Query, ManualAccountDetailsViewModel>
    {
        private readonly IAccountRepository _accountRepository;

        public GetMortgageDetailsHandler(IAccountRepository accountRepository, IMapper mapper, IDispatcher dispatcher) : base(mapper, dispatcher)
        {
            _accountRepository = accountRepository;
        }

        public override async Task<ManualAccountDetailsViewModel> Handle(GetMortgageDetails.Query request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetPropertyMortgageAccount(request.FinanceId, request.AccountId, cancellationToken);

            return account;
        }
    }
}