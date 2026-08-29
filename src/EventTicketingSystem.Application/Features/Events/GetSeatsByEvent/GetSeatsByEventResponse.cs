using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Application.Features.Events.GetSeatsByEvent
{
    public class GetSeatsByEventResponse
    {
        public int EventSeatId { get; set; }

        public int SeatId { get; set; }

        public string Section { get; set; } = string.Empty;

        public string Row { get; set; } = string.Empty;

        public int Number { get; set; }

        public decimal Price { get; set; }

        public EventSeatStatus Status { get; set; }
    }
}
