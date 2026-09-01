using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Features.Reservations.Common;
using EventTicketingSystem.Domain.Entities;
using EventTicketingSystem.Domain.Enums;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Reservations.CreateReservation
{
    public class CreateReservationCommandHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IValidator<CreateReservationCommand> _validator;
        private readonly IEventSeatRepository _eventSeatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReservationCommandHandler(
            IReservationRepository reservationRepository,
            IValidator<CreateReservationCommand> validator,
            IEventSeatRepository eventSeatRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _validator = validator;
            _eventSeatRepository = eventSeatRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReservationResponse> Handle(CreateReservationCommand createReservation, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(createReservation, cancellationToken);

            var eventSeat = await _eventSeatRepository.GetEventSeatById(createReservation.EventSeatId, cancellationToken);
            if (eventSeat == null)
            {
                throw new NotFoundException($"Event Seat With Id {createReservation.EventSeatId} Does Not Exist");
            }

            if (eventSeat.Status != EventSeatStatus.Available)
            {
                throw new SeatNotAvailableException($"Event seat with ID {eventSeat.Id} is not available.");
            }

            var now = DateTime.UtcNow;

            var reservation = new Reservation
            {
                UserId = createReservation.UserId,
                EventSeatId = createReservation.EventSeatId,
                CreatedAt = now,
                ExpiresAt = now.AddMinutes(10),
                Status = ReservationStatus.Active
            };

            eventSeat.Status = EventSeatStatus.Reserved;
            await _reservationRepository.AddAsync(reservation, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ReservationResponse
            {
                ReservationId = reservation.Id,
                EventSeatId = reservation.EventSeatId,
                ExpiresAt = reservation.ExpiresAt,
                Status = reservation.Status
            };
        }
    }
}
