using AutoMapper;
using Omikron.IdentityService.Domain.Queries;
using Omikron.IdentityService.Repositories;
using Omikron.IdentityService.ViewModel;
using Omikron.SharedKernel.Api.Models;
using Omikron.SharedKernel.Infrastructure.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Omikron.IdentityService.Domain.Handlers
{
    public class GetProfileDetailsQueryHandler : BaseHandlerLight<GetProfileDetails.Query, ApiResult<ProfileDetailsViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetProfileDetailsQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async override Task<ApiResult<ProfileDetailsViewModel>> Handle(GetProfileDetails.Query request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdIncludePhoneNumberAsync(request.Parameter, cancellationToken);

            return user == null ? ApiResult<ProfileDetailsViewModel>.NotFound("User not found.")
                                : ApiResult<ProfileDetailsViewModel>.Success().WithData(_mapper.Map<ProfileDetailsViewModel>(user));
        }
    }
}
