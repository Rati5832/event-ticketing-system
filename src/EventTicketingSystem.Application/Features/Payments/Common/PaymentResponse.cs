using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Application.Features.Payments.Common
{
    public class PaymentResponse
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public PaymentProvider Provider { get; set; }

        public string? TransactionId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
