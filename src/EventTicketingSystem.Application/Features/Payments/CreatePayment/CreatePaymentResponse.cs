using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Application.Features.Payments.CreatePayment
{
    public class CreatePaymentResponse
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public PaymentProvider Provider { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
