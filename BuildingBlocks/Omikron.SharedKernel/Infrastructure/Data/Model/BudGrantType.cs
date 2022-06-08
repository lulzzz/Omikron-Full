using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model
{
    public static class BudGrantType
    {
        public const string ClientCredentials = "client_credentials";
        public const string RefreshToken = "refresh_token";
    }
}
