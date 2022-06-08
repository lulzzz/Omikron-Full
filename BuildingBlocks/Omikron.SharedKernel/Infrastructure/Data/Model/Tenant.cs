using System;
using Omikron.SharedKernel.Domain;

namespace Omikron.SharedKernel.Infrastructure.Data.Model
{
    public sealed class Tenant : AggregateRoot<string>
    {
        public Tenant()
        {
            AzureAssetStatus = TenantAzureAssetStatus.New;
        }

        public Tenant(string id, string identifier, string name, string hostName, string logo, string cssFile, TenantStatus status, TenantType type) : this()
        {
            Id = id;
            Identifier = identifier;
            Name = name;
            HostName = hostName;
            Logo = logo;
            CssFile = cssFile;
            Status = status;
            Type = type;
            AzureAssetStatus = TenantAzureAssetStatus.Success;
        }

        public static Tenant SystemTenant => new Tenant(id: Constants.SystemTenantId, identifier: "Omikron", name: "Omikron Money Solution", hostName: "Omikron", logo: "images/applicita-software-logo.png", cssFile: null, status: TenantStatus.Active, type: TenantType.System);

        public string Identifier { get; set; }
        public string Name { get; set; }
        public string HostName { get; set; }
        public string Logo { get; set; }
        public string CssFile { get; set; }
        public TenantStatus Status { get; set; }
        public TenantType Type { get; set; }
        public TenantAzureAssetStatus AzureAssetStatus { get; set; }

        public bool IsSystemTenant()
        {
            return Identifier.Equals(value: SystemTenant.Identifier, comparisonType: StringComparison.CurrentCultureIgnoreCase);
        }

        public bool CheckIfReadyForUse()
        {
            return Status == TenantStatus.Active && AzureAssetStatus == TenantAzureAssetStatus.Success;
        }

        public static implicit operator OmikronTenantInfo(Tenant tenant)
        {
            return new OmikronTenantInfo
            {
                Id = tenant.Id,
                Identifier = tenant.Identifier,
                Name = tenant.Name
            };
        }
    }
}