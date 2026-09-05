namespace EventTicketingSystem.Application.Features.Helper
{
    public static class IdentifierGenerator
    {
        public static string GenerateBookingNumber()
        {
            return $"BK-{Guid.NewGuid().ToString("N")[..8].ToUpper()}"; ;
        }

        public static string GenerateTransactionId()
        {
            return $"TRANID-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }
    }
}
