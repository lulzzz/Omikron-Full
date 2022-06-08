using Omikron.SharedKernel.Infrastructure.Configuration;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;

namespace Omikron.SharedKernel.Infrastructure.Logging.Implementations.ApplicationInsights
{
    public class AppInsightsTelemetryInitialiser : ITelemetryInitializer
    {
        private readonly ApplicationInsightsConfiguration _applicationInsightsConfiguration;

        public AppInsightsTelemetryInitialiser(IOptions<ApplicationInsightsConfiguration> applicationInsightsConfiguration)
        {
            _applicationInsightsConfiguration = applicationInsightsConfiguration.Value;
        }

        public void Initialize(ITelemetry telemetry)
        { 
            // Correctly initialsing cloud role name will make filtering queries and charts on service much easier
            // It is also a key value used by appinsights application map
            telemetry.Context.Cloud.RoleName = 
                new CloudRoleName(_applicationInsightsConfiguration.CloudRoleName);
        }
    }
}
