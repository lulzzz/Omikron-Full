using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class RefreshCommandHandler : BaseHandler<Refresh.Command, ApiResult<RefreshHistoryViewModel>>
    {
        private readonly IRefreshHistoryRepository _refreshHistroryRepository;

        public RefreshCommandHandler(IDispatcher dispatcher, IRefreshHistoryRepository refreshHistroryRepository) : base(dispatcher)
        {
            _refreshHistroryRepository = refreshHistroryRepository;
        }

        public override async Task<ApiResult<RefreshHistoryViewModel>> Handle(Refresh.Command request, CancellationToken cancellationToken)
        {
            // Actual refresh of accounts using BUD should be initiated here

            var refresh = await _refreshHistroryRepository.AddRefresh(request.UserId, cancellationToken);

            return await _refreshHistroryRepository.SaveChangesAsync(cancellationToken) ?
                ApiResult<RefreshHistoryViewModel>.Success().WithData(new RefreshHistoryViewModel(refresh.CreatedAt.GetDifferenceStringFromNow())) :
                ApiResult<RefreshHistoryViewModel>.BadRequest("Failed to update database. Please try again later.");
        }
    }
}