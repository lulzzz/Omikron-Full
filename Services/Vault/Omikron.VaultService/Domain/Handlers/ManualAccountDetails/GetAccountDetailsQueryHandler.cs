using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries.ManualAccountDetails;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers.ManualAccountDetails
{
    public class GetAccountDetailsQueryHandler : BaseHandlerWithAutoMapper<GetAccountDetails.Query, ManualAccountDetailsViewModel>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountDetailsQueryHandler(IMapper mapper, IDispatcher dispatcher, IAccountRepository accountRepository) : base(mapper, dispatcher)
        {
            _accountRepository = accountRepository;
        }

        public override async Task<ManualAccountDetailsViewModel> Handle(GetAccountDetails.Query request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetManualAccountDetails(request.AccountId, cancellationToken);
            return account;
        }
    }
}