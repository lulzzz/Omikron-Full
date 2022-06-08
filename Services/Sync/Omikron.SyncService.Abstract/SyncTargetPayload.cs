namespace Omikron.Sync
{
    public class SyncTargetPayload<TValue> : SyncPayload<TValue>
    {
		public SyncTargetPayload(TValue value) : base(value)
		{
		}
    }
}