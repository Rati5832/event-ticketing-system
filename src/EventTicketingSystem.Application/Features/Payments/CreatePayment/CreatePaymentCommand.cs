using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Application.Features.Payments.CreatePayment
{
    public class CreatePaymentCommand
    {
        public int BookingId { get; set; }

        public PaymentProvider Provider { get; set; }
    }
}
