namespace EventTicketingSystem.Application.Common.Exceptions
{
    public class InvalidPaymentStateException : Exception
    {
        public InvalidPaymentStateException(string message) : base(message)
        {

        }
    }
}
