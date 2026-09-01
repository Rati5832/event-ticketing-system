using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Application.Features.Reservations.Common
{
    public class ReservationResponse
    {
        public int ReservationId { get; set; }

        public int EventSeatId { get; set; }

        public DateTime ExpiresAt { get; set; }

        public ReservationStatus Status { get; set; }
    }
}
