using System;
using System.Net.Http;

namespace Omikron.VaultService.Infrastructure.UkVehicleData
{
    public class UkVehicleSettings
    {
        public string BaseUrl { get; set; }
        public string ValuationEndpoint { get; set; }
        public string ApiKey { get; set; }
    }
}