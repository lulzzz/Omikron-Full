using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using System.Collections.Generic;

namespace Omikron.IdentityService.ViewModel
{
    public class ObProviderViewModel
    {
        public string Provider { get; set; }

        public string DisplayName { get; set; }

        public ObProviderMaintenanceWindow MaintenanceWindow { get; set; }

        public string MaintenanceStatus { get; set; }

        public string Icon { get; set; }

        public List<string> Regions { get; set; }
    }

    public class ObProviderProfile : Profile
    {
        public ObProviderProfile()
        {
            CreateMap<ObProvidersResponse, ObProviderViewModel>()
                .ReverseMap();
        }
    }
}
