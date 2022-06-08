using System;
using System.Threading.Tasks;

namespace Omikron.SharedKernel.Infrastructure.Asset
{
    public interface IAssetManager
    {
        Task<SqlDatabaseAssetResponse> CreateDatabaseAsync(CreateSqlDatabaseAssetRequest request);
        Task DeleteDatabaseAsync(string databaseName);
        Task<Uri> GetDatabaseResourceUriAsync(string databaseName);
    }
}