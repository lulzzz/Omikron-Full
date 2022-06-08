namespace Omikron.SharedKernel.Infrastructure.SignalR.Signals
{
    /// <summary>
    ///     A response wrapper class will be sent to the client once when the signal has been sent.
    /// </summary>
    public class Response
    {
        /// <summary>
        ///     Represent the response that signal has been accepted.
        /// </summary>
        public static Response Accepted() => new Response(SignalStatus.Accepted, "The signal has been accepted.");
        /// <summary>
        ///     Represent the response that signal has been accepted.
        /// </summary>
        public static Response Accepted(object payload) => new Response(SignalStatus.Accepted, "The signal has been accepted.", payload);
        /// <summary>
        ///     Represent the response that signal has been rejected.
        /// </summary>
        public static Response Rejected(string reason = null) => new Response(SignalStatus.Rejected, reason ?? "The signal has been rejected.");
        /// <summary>
        ///     Represent the response with error.
        /// </summary>
        public static Response Error(string error = null) => new Response(SignalStatus.Error, error ?? "The error has occurred.");
        public SignalStatus Status { get; }
        public string Message { get; }
        public object Payload { get; }
        protected Response(SignalStatus status, string message, object payload = null)
        {
            Status = status;
            Message = message;
            Payload = payload;
        }
    }
}