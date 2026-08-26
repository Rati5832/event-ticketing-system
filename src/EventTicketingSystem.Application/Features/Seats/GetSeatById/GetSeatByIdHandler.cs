using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Features.Events.CreateEvent;
using EventTicketingSystem.Application.Features.Seats.Common;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Seats.GetSeatById
{
    public class GetSeatByIdHandler
    {
        private readonly ISeatRepository _seatRepository;
        private readonly GetSeatByIdValidator _requestValidation;

        public GetSeatByIdHandler(ISeatRepository seatRepository, GetSeatByIdValidator requestValidation)
        {
            _seatRepository = seatRepository;
            _requestValidation = requestValidation;
        }

        public async Task<SeatResponse> HandleAsync(GetSeatByIdRequest seatRequest, CancellationToken cancellationToken = default)
        {
            await _requestValidation.ValidateAndThrowAsync(seatRequest, cancellationToken);

            var seatEntity = await _seatRepository.GetByIdAsync(seatRequest.Id, cancellationToken);

            if (seatEntity == null)
            {
                throw new NotFoundException($"Seat with id {seatRequest.Id} not found.");
            }

            return new SeatResponse
            {
                Id = seatEntity.Id,
                VenueId = seatEntity.VenueId,
                Section = seatEntity.Section,
                Row = seatEntity.Row,
                Number = seatEntity.Number,
            };
        }
    }
}
