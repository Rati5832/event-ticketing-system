namespace EventTicketingSystem.Application.Common.Exceptions
{
    public class InvalidReservationStateException : Exception
    {
        public InvalidReservationStateException(string message) : base(message)
        { 
        }
    }
}
