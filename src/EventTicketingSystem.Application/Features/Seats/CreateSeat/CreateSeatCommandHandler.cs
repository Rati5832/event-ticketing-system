using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Features.Seats.Common;
using EventTicketingSystem.Domain.Entities;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Seats.CreateSeat
{
    public class CreateSeatCommandHandler
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVenueRepository _venueRepository;
        private readonly IValidator<CreateSeatCommand> _validator;

        public CreateSeatCommandHandler(
            ISeatRepository seatRepository, 
            IUnitOfWork unitOfWork,
            IVenueRepository venueRepository,
            IValidator<CreateSeatCommand> validator)
        {
            _seatRepository = seatRepository;
            _unitOfWork = unitOfWork;
            _venueRepository = venueRepository;
            _validator = validator;
        }

        public async Task<SeatResponse> HandleAsync(int venueId, CreateSeatRequest seatRequest, CancellationToken cancellationToken = default)
        {
            var command = new CreateSeatCommand
            {
                VenueId = venueId,
                Section = seatRequest.Section,
                Row = seatRequest.Row,
                Number = seatRequest.Number
            };

            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var venue = await _venueRepository.GetByIdAsync(venueId, cancellationToken);

            if (venue == null)
            {
                throw new NotFoundException($"Venue with ID {venueId} does not exist.");
            }

            var seatExists = await _seatRepository.ExistsAsync(new Seat
            {
                VenueId = venueId,
                Section = seatRequest.Section,
                Row = seatRequest.Row,
                Number = seatRequest.Number,
            }, cancellationToken);

            if (!seatExists)
            {
                throw new SeatAlreadyExistsException("A seat with the same section, row, and number already exists in this venue.");
            }

            var seatEntity = new Seat
            {
                VenueId = venueId,
                Section = command.Section,
                Row = command.Row,
                Number = command.Number,
            };

            await _seatRepository.AddAsync(seatEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

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
