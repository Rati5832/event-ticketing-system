using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public int ReservationId { get; set; }

        public Reservation Reservation { get; set; } = null!;

        public string BookingNumber { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public BookingStatus Status { get; set; }

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}