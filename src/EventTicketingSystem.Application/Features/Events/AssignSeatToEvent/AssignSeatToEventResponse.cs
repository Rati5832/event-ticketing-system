using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Application.Features.Events.AssignSeatToEvent
{
    public class AssignSeatToEventResponse
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int SeatId { get; set; }

        public decimal Price { get; set; }

        public EventSeatStatus Status { get; set; }

    }
}
