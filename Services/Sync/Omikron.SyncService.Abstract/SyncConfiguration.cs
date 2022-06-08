namespace Omikron.Sync
{
    public class SyncConfiguration
    {
        public SyncConfiguration(SyncInterval interval)
        {
            Interval = interval;
        }

        public SyncInterval Interval { get; set; }
    }
}