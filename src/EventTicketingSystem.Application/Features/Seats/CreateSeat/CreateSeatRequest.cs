namespace EventTicketingSystem.Application.Features.Seats.CreateSeat
{
    public class CreateSeatRequest
    {
        public string Section { get; set; } = string.Empty;

        public string Row { get; set; } = string.Empty;

        public int Number { get; set; }
    }
}
