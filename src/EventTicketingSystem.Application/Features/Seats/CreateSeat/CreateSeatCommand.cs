namespace EventTicketingSystem.Application.Features.Seats.CreateSeat
{
    public class CreateSeatCommand
    {
        public int VenueId {  get; set; }

        public string Section { get; set; } = string.Empty;

        public string Row { get; set; } = string.Empty;

        public int Number { get; set; }
    }
}
