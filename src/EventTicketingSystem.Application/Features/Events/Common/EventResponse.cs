using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Application.Features.Events.CreateEvent
{
    public class EventResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int VenueId { get; set; }

        public EventStatus Status { get; set; }
    }
}
