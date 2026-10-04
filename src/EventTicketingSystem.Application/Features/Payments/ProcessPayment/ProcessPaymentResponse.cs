using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Application.Features.Payments.ProcessPayment
{
    public class ProcessPaymentResponse
    {
        public int PaymentId { get; set; }

        public int BookingId { get; set; }
        
        public PaymentStatus PaymentStatus { get; set; }
        
        public BookingStatus BookingStatus { get; set; }
    }
}
