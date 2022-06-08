using Omikron.SharedKernel.Domain;

namespace Omikron.Sync
{
    public sealed class SyncStatus : Enumeration
    {
        public SyncStatus(int id, string name) : base(id: id, name: name)
        {
        }

        public static SyncStatus None => new(id: 0, name: "None");
        public static SyncStatus Error => new(id: 1, name: "Error");
        public static SyncStatus Success => new(id: 2, name: "Success");

        public static implicit operator int(SyncStatus value)
        {
            return value.Id;
        }
    }
}