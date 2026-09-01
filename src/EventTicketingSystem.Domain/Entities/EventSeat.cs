using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Domain.Entities
{
    public class EventSeat
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; } = null!;

        public int SeatId { get; set; }

        public Seat Seat { get; set; } = null!;

        public decimal Price { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public EventSeatStatus Status { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}