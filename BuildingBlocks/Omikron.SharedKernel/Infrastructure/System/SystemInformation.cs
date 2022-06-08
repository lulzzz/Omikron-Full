using System.Text;

namespace Omikron.SharedKernel.Infrastructure.System
{
    public class SystemInformation
    {
        public bool IsUserLoggedIn { get; set; }
        public string Username { get; set; }
        public string Tenant { get; set; }
        public string RequestId { get; set; }
        public string IpAddress { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"IsUserLoggedIn = {IsUserLoggedIn}");
            sb.AppendLine($"Username = {Username}");
            sb.AppendLine($"Tenant = {Tenant}");
            sb.AppendLine($"IpAddress = {IpAddress}");
            sb.AppendLine($"RequestId = {RequestId}");

            return sb.ToString();
        }
    }
}