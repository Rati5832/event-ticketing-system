using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Features.Events.CreateEvent;

namespace EventTicketingSystem.Application.Features.Events.GetEvents
{
    public class GetEventsHandler
    {
        private readonly IEventRepository _eventRepository;

        public GetEventsHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<EventResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            return (await _eventRepository.GetAllAsync(cancellationToken))
                .Select(eventEntity => new EventResponse
                {
                    Id = eventEntity.Id,
                    Name = eventEntity.Name,
                    Description = eventEntity.Description,
                    StartDate = eventEntity.StartDate,
                    EndDate = eventEntity.EndDate,
                    VenueId = eventEntity.VenueId,
                    Status = eventEntity.Status
                })
                .ToList();
        }
    }
}
