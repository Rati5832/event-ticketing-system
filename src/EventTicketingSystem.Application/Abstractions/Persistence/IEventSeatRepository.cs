using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Application.Abstractions.Persistence
{
    public interface IEventSeatRepository
    {
        Task AssignSeatToEventAsync(EventSeat eventSeat, CancellationToken cancellationToken);

        Task<bool> EventAndSeatExistsAsync(int eventId, int seatId, CancellationToken cancellationToken);

        Task<EventSeat?> GetEventSeatById(int eventSeatId, CancellationToken cancellationToken);
    }
}
