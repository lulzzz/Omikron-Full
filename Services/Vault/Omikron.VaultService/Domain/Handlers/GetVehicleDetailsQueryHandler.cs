using AutoMapper;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.VaultService.Domain.Handlers
{
    public class GetVehicleDetailsQueryHandler : BaseHandlerWithAutoMapper<GetVehicleDetails.Query, ApiResult<VehicleViewModel>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleDetailsQueryHandler(IMapper mapper, IDispatcher dispatcher, IVehicleRepository vehicleRepository) : base(mapper, dispatcher)
        {
            _vehicleRepository = vehicleRepository;
        }

        public override async Task<ApiResult<VehicleViewModel>> Handle(GetVehicleDetails.Query request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetVehicleDetails(request.AccountId, cancellationToken);
            return vehicle == null ? ApiResult<VehicleViewModel>.NotFound() : ApiResult<VehicleViewModel>.Success().WithData(vehicle);
        }
    }
}