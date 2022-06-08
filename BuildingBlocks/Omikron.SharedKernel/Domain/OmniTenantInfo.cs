using System.Collections.Generic;

namespace Omikron.SharedKernel.Domain
{
    public class OmikronTenantInfo
    {
        public OmikronTenantInfo()
        {
            Items = new Dictionary<string, object>();
        }

        public OmikronTenantInfo(string id, string identifier, string name)
        {
            Id = id;
            Identifier = identifier;
            Name = name;
            Items = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Items { get; set; }
        public string Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
    }
}