namespace EventTicketingSystem.Application.Features.Bookings.Helper
{
    public static class BookingHelper
    {
        public static string GenerateBookingNumber()
        {
            return $"BK-{Guid.NewGuid().ToString("N")[..8].ToUpper()}"; ;
        }
    }
}
