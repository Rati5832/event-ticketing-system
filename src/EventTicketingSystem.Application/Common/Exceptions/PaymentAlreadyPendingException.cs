namespace EventTicketingSystem.Application.Common.Exceptions
{
    public class PaymentAlreadyPendingException : Exception
    {
        public PaymentAlreadyPendingException(string message) : base(message)
        {
            
        }

        public PaymentAlreadyPendingException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
