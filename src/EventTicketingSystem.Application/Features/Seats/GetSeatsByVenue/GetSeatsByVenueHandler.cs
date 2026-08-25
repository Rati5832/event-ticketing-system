using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Features.Seats.Common;
using EventTicketingSystem.Application.Features.Venues.GetVenueById;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Seats.GetSeatsByVenue
{
    public class GetSeatsByVenueHandler
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IValidator<GetVenueByIdRequest> _validator;

        public GetSeatsByVenueHandler(
            ISeatRepository seatRepository,
            IVenueRepository venueRepository,
            IValidator<GetVenueByIdRequest> validator)
        {
            _seatRepository = seatRepository;
            _venueRepository = venueRepository;
            _validator = validator;
        }

        public async Task<List<SeatResponse>> HandleAsync(GetVenueByIdRequest venueRequest, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(venueRequest, cancellationToken);

            var venue = await _venueRepository.GetByIdAsync(venueRequest.Id, cancellationToken);

            if (venue == null)
            {
                throw new NotFoundException($"Venue with ID {venueRequest.Id} does not exist.");
            }

            var seats = await _seatRepository.GetAllByVenueAsync(venueRequest.Id, cancellationToken);

            return seats.Select(seat => new SeatResponse
            {
                Id = seat.Id,
                Row = seat.Row,
                Number = seat.Number,
                Section = seat.Section,
                VenueId = seat.VenueId
            }).ToList();

        }
    }
}
