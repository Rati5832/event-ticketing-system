using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Domain.Entities;
using EventTicketingSystem.Domain.Enums;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Events.CreateEvent
{
    public class CreateEventHandler
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateEventRequestValidator _requestValidation;

        public CreateEventHandler(IVenueRepository venueRepository, IEventRepository eventRepository, IUnitOfWork unitOfWork, CreateEventRequestValidator requestValidation)
        {
            _venueRepository = venueRepository;
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _requestValidation = requestValidation;
        }

        public async Task<CreateEventResponse> HandleAsync(CreateEventRequest eventRequest, CancellationToken cancellationToken = default)
        {
            await _requestValidation.ValidateAndThrowAsync(eventRequest, cancellationToken);

            var venue = await _venueRepository.GetByIdAsync(eventRequest.VenueId, cancellationToken);

            if (venue == null)
            {
                throw new NotFoundException($"Can not create event. Venue with id {eventRequest.VenueId} not found.");
            }

            var eventEntity = new Event
            {
                Name = eventRequest.Name,
                Description = eventRequest.Description,
                StartDate = eventRequest.StartDate,
                EndDate = eventRequest.EndDate,
                VenueId = eventRequest.VenueId,
                Status = EventStatus.Scheduled
            };

            await _eventRepository.AddAsync(eventEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateEventResponse
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                VenueId = eventEntity.VenueId,
                Status = eventEntity.Status,
            };
        }
    }
}
