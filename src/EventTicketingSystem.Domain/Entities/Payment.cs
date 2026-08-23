using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public Booking Booking { get; set; } = null!;

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public string Provider { get; set; } = string.Empty;

        public string? TransactionId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}