namespace Omikron.SharedKernel.Exceptions
{
    public sealed class BudApiException : ApiException
    {
        public BudApiException(string message) : base(message: message)
        {
        }
    }
}