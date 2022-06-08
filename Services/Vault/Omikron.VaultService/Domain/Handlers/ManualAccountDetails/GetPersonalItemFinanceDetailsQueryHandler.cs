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
    public class GetPersonalItemFinanceDetailsQueryHandler : BaseHandlerWithAutoMapper<GetPersonalItemFinanceDetails.Query, ManualAccountDetailsViewModel>
    {
        private readonly IAccountRepository _accountRepository;

        public GetPersonalItemFinanceDetailsQueryHandler(IMapper mapper, IDispatcher dispatcher, IAccountRepository accountRepository) : base(mapper, dispatcher)
        {
            _accountRepository = accountRepository;
        }

        public override async Task<ManualAccountDetailsViewModel> Handle(GetPersonalItemFinanceDetails.Query request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetPersonalItemFinanceAccount(request.FinanceId,request.AccountId, cancellationToken);
            return account;
        }
    }
}