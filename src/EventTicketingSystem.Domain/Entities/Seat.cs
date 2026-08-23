namespace EventTicketingSystem.Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }

        public int VenueId { get; set; }

        public Venue Venue { get; set; } = null!;

        public string Section { get; set; } = string.Empty;

        public string Row { get; set; } = string.Empty;

        public int Number { get; set; }

        public ICollection<EventSeat> EventSeats { get; set; } = new List<EventSeat>();
    }
}