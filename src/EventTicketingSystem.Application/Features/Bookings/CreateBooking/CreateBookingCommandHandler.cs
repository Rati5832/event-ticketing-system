using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Features.Bookings.Common;
using EventTicketingSystem.Application.Features.Helper;
using EventTicketingSystem.Domain.Entities;
using EventTicketingSystem.Domain.Enums;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Bookings.CreateBooking
{
    public class CreateBookingCommandHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateBookingCommand> _validator;

        public CreateBookingCommandHandler(
            IReservationRepository reservationRepository, 
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreateBookingCommand> validator)
        {
            _reservationRepository = reservationRepository;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<BookingResponse> HandleAsync(CreateBookingCommand createBooking, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(createBooking, cancellationToken);

            var reservation = await _reservationRepository.GetByIdAsync(createBooking.ReservationId, cancellationToken);

            if (reservation == null)
            {
                throw new NotFoundException($"Reservation with ID {createBooking.ReservationId} not found.");
            }

            if (reservation.Status != ReservationStatus.Active)
            {
                throw new InvalidReservationStateException("Reservation is not active.");
            }

            if (reservation.ExpiresAt <= DateTime.UtcNow)
            {
                throw new InvalidReservationStateException("Reservation is expired.");
            }

            if (reservation.EventSeat.Status != EventSeatStatus.Reserved)
            {
                throw new InvalidReservationStateException("Event Seat Is Not Reserved.");
            }

            if (await _bookingRepository.ExistsByReservationIdAsync(reservation.Id, cancellationToken))
            {
                throw new InvalidReservationStateException("Booking already exists for this reservation.");
            }

            var booking = new Booking
            {
                UserId = reservation.UserId,
                ReservationId = reservation.Id,
                BookingNumber = IdentifierGenerator.GenerateBookingNumber(),
                TotalPrice = reservation.EventSeat.Price,
                CreatedAt = DateTime.UtcNow,
                Status = BookingStatus.Pending
            };

            await _bookingRepository.AddAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BookingResponse
            {
                Id = booking.Id,
                UserId = booking.UserId,
                ReservationId = booking.ReservationId,
                BookingNumber = booking.BookingNumber,
                TotalPrice = booking.TotalPrice,
                CreatedAt = booking.CreatedAt,
                Status = booking.Status
            };
        }
    }
}
