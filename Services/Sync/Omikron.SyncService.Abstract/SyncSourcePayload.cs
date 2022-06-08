namespace Omikron.Sync
{
	public class SyncSourcePayload<TValue> : SyncPayload<TValue>
	{
		public SyncSourcePayload(TValue value) : base(value)
		{
		}
	}
}