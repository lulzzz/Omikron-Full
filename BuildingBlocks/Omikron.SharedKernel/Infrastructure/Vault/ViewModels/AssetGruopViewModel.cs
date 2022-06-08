using System.Collections.Generic;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class AssetGruopViewModel
    {
        public AssetType AssetType { get; set; }
        public int Count { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<AssetViewModel> Assets { get; set; }
        public string AssetTypeName { get; set; }
    }
}