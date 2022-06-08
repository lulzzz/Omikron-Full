using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Repository.Abstract;
using Omikron.SharedKernel.Infrastructure.Vault.ViewModels;
using Omikron.VaultService.Domain.Queries;

namespace Omikron.VaultService.Domain.Handlers
{
    public class GetPropertyDetailsQueryHandler : BaseHandlerWithAutoMapper<GetPropertyDetails.Query, ApiResult<PropertyViewModel>>
    {
        private readonly IPropertyRepository _propertyRepository;

        public GetPropertyDetailsQueryHandler(IMapper mapper, IDispatcher dispatcher, IPropertyRepository propertyRepository) : base(mapper, dispatcher)
        {
            _propertyRepository = propertyRepository;
        }

        public override async Task<ApiResult<PropertyViewModel>> Handle(GetPropertyDetails.Query request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetPropertyDetails(request.AccountId, cancellationToken);
            return property == null ? ApiResult<PropertyViewModel>.NotFound() : ApiResult<PropertyViewModel>.Success().WithData(property);
        }
    }
}