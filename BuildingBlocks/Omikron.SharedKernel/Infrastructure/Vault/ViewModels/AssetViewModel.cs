using System;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class AssetViewModel
    {
        public Guid HostId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}