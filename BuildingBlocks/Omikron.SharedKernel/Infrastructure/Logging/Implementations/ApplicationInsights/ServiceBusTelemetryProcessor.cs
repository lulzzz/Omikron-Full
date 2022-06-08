using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using System;

namespace Omikron.SharedKernel.Infrastructure.Logging.Implementations.ApplicationInsights
{
    public class ServiceBusTelemetryProcessor : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; set; }

        private bool _enable;

        public ServiceBusTelemetryProcessor(ITelemetryProcessor next, IConfiguration configuration)
        {
            _enable = configuration.GetValue<bool>("Logging:ApplicationInsights:EnableServiceBusEventsFilter", true);
            this.Next = next;
        }

        public void Process(ITelemetry item)
        {
            if (_enable && FilterServiceBusTelemetry(item)) 
            { 
                return; 
            }

            this.Next.Process(item);
        }

 
        /// <summary>
        /// Should we filter out a service bus dependency from application insights
        /// Filter out receive events with no valid messageid, these are simply polls to the service bus queue/topic
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool FilterServiceBusTelemetry(ITelemetry item)
        {
            var dependencyItem = item as DependencyTelemetry;
            if (dependencyItem != null && dependencyItem.Type == DependencyTelemetryTypes.AZURE_SERVICE_BUS)
            {
                if(dependencyItem.Success.HasValue && dependencyItem.Success == false)
                {
                    // Always log errors
                    return false;
                }
                else if (dependencyItem.Name == DependencyTelemetryNames.RECEIVE && !dependencyItem.Properties.ContainsKey(DependencyTelemetryProperties.MESSAGE_ID))
                {  
                    // Just a poll
                    return true;
                }
                else
                {
                    // Valid receive event
                    return false;
                }
            }
            else
            {
                // Not a service bus dependency 
                return false;
            }
        }
    }
}
