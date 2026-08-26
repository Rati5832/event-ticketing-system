namespace EventTicketingSystem.Application.Common.Exceptions
{
    public class SeatNotInVenueException : Exception
    {
        public SeatNotInVenueException(string message) : base(message)
        {
        }
    }
}
