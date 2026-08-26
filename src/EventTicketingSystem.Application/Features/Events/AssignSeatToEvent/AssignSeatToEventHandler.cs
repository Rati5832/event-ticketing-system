using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Features.Seats.CreateSeat;
using EventTicketingSystem.Domain.Entities;
using EventTicketingSystem.Domain.Enums;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Events.AssignSeatToEvent
{
    public class AssignSeatToEventHandler
    {
        private readonly IEventRepository _eventRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IEventSeatRepository _eventSeatRepository;
        private readonly IValidator<AssignSeatToEventCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public AssignSeatToEventHandler(
            IEventRepository eventRepository, 
            ISeatRepository seatRepository,
            IEventSeatRepository eventSeatRepository, 
            IValidator<AssignSeatToEventCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _seatRepository = seatRepository;
            _eventSeatRepository = eventSeatRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<AssignSeatToEventResponse> HandleAsync(int eventId, AssignSeatToEventRequest createSeatRequest, CancellationToken cancellationToken = default)
        {
            var assignSeatToEventCommand = new AssignSeatToEventCommand
            {
                EventId = eventId,
                SeatId = createSeatRequest.SeatId,
                Price = createSeatRequest.Price,
            };

            await _validator.ValidateAndThrowAsync(assignSeatToEventCommand, cancellationToken);

            var eventEntity = await _eventRepository.GetByIdAsync(eventId, cancellationToken);
            if (eventEntity == null)
            {
                throw new NotFoundException($"Event With Id {eventId} Does Not Exist.");
            }

            var seatEntity = await _seatRepository.GetByIdAsync(createSeatRequest.SeatId, cancellationToken);
            if (seatEntity == null)
            {
                throw new NotFoundException($"Seat With Id {createSeatRequest.SeatId} Does Not Exist.");
            }
            
            if (eventEntity.VenueId != seatEntity.VenueId)
            {
                throw new SeatNotInVenueException($"Event And Seat Should Be Under Same Venue");
            }

            var eventSeatAlreadyExists = await _eventSeatRepository.EventAndSeatExistsAsync(eventId, createSeatRequest.SeatId, cancellationToken);

            if (eventSeatAlreadyExists)
            {
                throw new SeatAlreadyExistsException($"Event With Id: {eventId}, Already Has Seat With Id: {createSeatRequest.SeatId}");
            }

            var eventSeatEntity = new EventSeat
            {
                EventId = assignSeatToEventCommand.EventId,
                SeatId = assignSeatToEventCommand.SeatId,
                Price = assignSeatToEventCommand.Price,
                Status = EventSeatStatus.Available,
            };

            await _eventSeatRepository.AssignSeatToEventAsync(eventSeatEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AssignSeatToEventResponse
            {
                Id = eventSeatEntity.Id,
                EventId = eventSeatEntity.EventId,
                SeatId = eventSeatEntity.SeatId,
                Price = eventSeatEntity.Price,
                Status = eventSeatEntity.Status,
            };
        }
    }
}
