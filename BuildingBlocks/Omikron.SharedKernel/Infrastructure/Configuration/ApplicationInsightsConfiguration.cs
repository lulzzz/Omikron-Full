using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Configuration
{
    public sealed class ApplicationInsightsConfiguration
    {
        public string Key {get; set;}

        public bool EnableDependencyTrackingTelemetryModule { get; set; }
        
        public bool EnableRequestTrackingTelemetryModule { get; set; }
        
        public bool EnableSampling { get; set; }
        
        public string CloudRoleName {get; set;}
        
        public bool EnableServiceBusEventsFilter { get; set; }
    }
}
