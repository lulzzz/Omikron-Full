using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Extensions;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Commands;
using Omikron.VaultService.Domain.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
	public class GetLastRefreshQueryHandler : BaseHandler<GetLastRefresh.Query, ApiResult<RefreshHistoryViewModel>>
    {
        private readonly IRefreshHistoryRepository _refreshHistroryRepository;

        public GetLastRefreshQueryHandler(IDispatcher dispatcher, IRefreshHistoryRepository refreshHistroryRepository) : base(dispatcher)
        {
            _refreshHistroryRepository = refreshHistroryRepository;
        }

        public override async Task<ApiResult<RefreshHistoryViewModel>> Handle(GetLastRefresh.Query request, CancellationToken cancellationToken)
        {
            var refreshHistory = await _refreshHistroryRepository.GetLastRefresh(request.UserId, cancellationToken);

            // This could be case only while we have fake implementation
            // In real implementation first refresh history should be created when first sync with BUD API data has been performed
            if (refreshHistory == null)
            {
                var addRefreshCommand = new Refresh.Command(request.UserId);
                var addRefreshSuccess = await Dispatcher.DispatchAsync(addRefreshCommand, cancellationToken);

                if (!addRefreshSuccess.IsSuccess)
                {
                    return ApiResult<RefreshHistoryViewModel>.BadRequest("Something went wrong. Please try again");
                }

                refreshHistory = await _refreshHistroryRepository.GetLastRefresh(request.UserId, cancellationToken);
                if (refreshHistory == null)
                {
                    return ApiResult<RefreshHistoryViewModel>.NotFound("There are no refresh data for this user. Please add your first account.");
                }

                // Use only this for real implementation
                //return ApiResult<RefreshHistoryViewModel>.NotFound("There are no refresh data for this user. Please add your first account.");
            }

            return ApiResult<RefreshHistoryViewModel>.Success().WithData(new RefreshHistoryViewModel(refreshHistory.CreatedAt.GetDifferenceStringFromNow()));
        }
    }
}