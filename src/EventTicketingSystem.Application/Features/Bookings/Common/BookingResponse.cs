using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Application.Features.Bookings.Common
{
    public class BookingResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ReservationId { get; set; }

        public string BookingNumber { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public BookingStatus Status { get; set; }

    }
}
