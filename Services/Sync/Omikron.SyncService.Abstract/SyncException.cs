using System;

namespace Omikron.Sync
{
    [Serializable]
    public class SyncException : Exception
    {
        public SyncException(string message, Exception inner) : base(message: message, innerException: inner)
        {
        }

        public static SyncException None => new SyncException(message: string.Empty, inner: null);
    }
}