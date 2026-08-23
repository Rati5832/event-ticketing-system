using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Features.Events.CreateEvent;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Events.GetEventById
{
    public class GetEventByIdHandler
    {
        private readonly IEventRepository _eventRepository;
        private readonly GetEventByIdValidator _requestValidation;

        public GetEventByIdHandler(IEventRepository eventRepository, GetEventByIdValidator requestValidation)
        {
            _eventRepository = eventRepository;
            _requestValidation = requestValidation;
        }

        public async Task<EventResponse> HandleAsync(GetEventByIdRequest eventRequest, CancellationToken cancellationToken = default)
        {
            await _requestValidation.ValidateAndThrowAsync(eventRequest, cancellationToken);

            var eventEntity = await _eventRepository.GetByIdAsync(eventRequest.Id, cancellationToken);

            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with id {eventRequest.Id} not found.");
            }

            return new EventResponse
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                VenueId = eventEntity.VenueId,
                Status = eventEntity.Status
            };
        }
    }
}
