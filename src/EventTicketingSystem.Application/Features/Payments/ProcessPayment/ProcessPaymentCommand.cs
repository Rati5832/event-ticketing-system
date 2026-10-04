namespace EventTicketingSystem.Application.Features.Payments.ProcessPayment
{
    public class ProcessPaymentCommand
    {
        public int PaymentId { get; set; }

        public bool IsSuccessful { get; set; }
    }
}
