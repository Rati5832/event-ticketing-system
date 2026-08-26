namespace EventTicketingSystem.Application.Features.Events.AssignSeatToEvent
{
    public class AssignSeatToEventCommand
    {
        public int EventId { get; set; }

        public int SeatId { get; set; }

        public int Price { get; set; }
    }
}
