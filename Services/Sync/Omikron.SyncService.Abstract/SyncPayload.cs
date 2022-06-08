namespace Omikron.Sync
{
	public abstract class SyncPayload<TValue>
	{
		public TValue Value { get; set; }

		protected SyncPayload(TValue value)
		{
			Value = value;
		}
	}
}