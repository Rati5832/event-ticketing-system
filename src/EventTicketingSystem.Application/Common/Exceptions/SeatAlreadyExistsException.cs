namespace EventTicketingSystem.Application.Common.Exceptions
{
    public class SeatAlreadyExistsException : Exception
    {
        public SeatAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
