namespace EventTicketingSystem.Application.Common.Exceptions
{
    public class SeatNotAvailableException : Exception
    {
        public SeatNotAvailableException(string message) : base(message)
        {
        }
    }
}
