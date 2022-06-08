using System;
using System.Collections.Generic;

namespace Omikron.SharedKernel.Infrastructure.Configuration
{
    public class ApiServices
    {
        public const string ApiServicesTwilioConfigurationKey = "ApiServices:Twilio";
        public const string ApiServicesBudConfigurationKey = "ApiServices:Bud";
        public TwilioConfiguration Twilio { get; set; }
        public BudConfiguration Bud { get; set; }

        //TODO: Adapt postcode implementation to this
    }

    public class BudConfiguration
    {
        public Uri ApiUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string LoggingStorageConnectionString { get; set; }
        public string Container { get; set; }
    }

    public class TwilioConfiguration
    {
        public string AccountSid { get; set; } = "AC8e35a43f85e5b114fd355385d2af81ab";
        public string AuthToken { get; set; } = "ebe5141373186a9619057be6584b9ea6";
        public string PhoneNumber { get; set; } = "+447782375852";
    }

    public class BudProviderIcons
    {
        public string DefaultBackground { get; set; }
        public Dictionary<string, BudProviderIcon> Providers { get; set; }
    }

    public class BudProviderIcon
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Provider { get; set; }
        public string Colour { get; set; }
    }
}