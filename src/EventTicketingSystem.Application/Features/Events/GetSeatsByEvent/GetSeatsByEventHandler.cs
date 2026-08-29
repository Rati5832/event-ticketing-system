using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Features.Events.GetEventById;
using EventTicketingSystem.Domain.Entities;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Events.GetSeatsByEvent
{
    public class GetSeatsByEventHandler
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<GetEventByIdRequest> _validator;

        public GetSeatsByEventHandler(
            ISeatRepository seatRepository,
            IEventRepository eventRepository,
            IValidator<GetEventByIdRequest> validator)
        {
            _seatRepository = seatRepository;
            _eventRepository = eventRepository;
            _validator = validator;
        }

        public async Task<List<GetSeatsByEventResponse>> HandleAsync(GetEventByIdRequest getEventById, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(getEventById, cancellationToken);

            var eventEntity = await _eventRepository.GetByIdAsync(getEventById.Id, cancellationToken);
            
            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with ID: {getEventById.Id} does not exist.");
            }

            var seatsByEvent = await _seatRepository.GetAllByEventAsync(getEventById.Id, cancellationToken);

            return seatsByEvent.SelectMany(
            s => s.EventSeats,
            (s, es) => new GetSeatsByEventResponse
            {
                EventSeatId = es.Id,
                SeatId = s.Id,
                Section = s.Section,
                Row = s.Row, 
                Number = s.Number, 
                Price = es.Price,
                Status = es.Status 
    
             }).ToList();
        }
    }
}
