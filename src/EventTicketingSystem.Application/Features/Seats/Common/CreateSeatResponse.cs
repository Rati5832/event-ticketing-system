namespace EventTicketingSystem.Application.Features.Seats.Common
{
    public class CreateSeatResponse
    {
        public int Id { get; set; }

        public int VenueId { get; set; }

        public string Section { get; set; } = string.Empty;

        public string Row { get; set; } = string.Empty;

        public int Number { get; set; }
    }
}
