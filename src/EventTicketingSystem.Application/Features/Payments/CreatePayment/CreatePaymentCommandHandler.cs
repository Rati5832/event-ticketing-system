using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Common.Exceptions.UniqueConstraint;
using EventTicketingSystem.Domain.Entities;
using EventTicketingSystem.Domain.Enums;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Payments.CreatePayment
{
    public class CreatePaymentCommandHandler
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreatePaymentCommand> _validator;

        public CreatePaymentCommandHandler(
            IPaymentRepository paymentRepository,
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreatePaymentCommand> validator)
        {
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<CreatePaymentResponse> HandleAsync(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var booking = await _bookingRepository.GetBookByIdAsync(command.BookingId, cancellationToken);

            if (booking == null)
            {
                throw new NotFoundException($"Booking with ID {command.BookingId} not found.");
            }

            if (booking.Status != BookingStatus.Pending)
            {
                throw new InvalidBookingException($"Cannot create payment for booking with ID {command.BookingId} because the booking is not pending.");
            }

            if (booking.Reservation.Status != ReservationStatus.Active)
            {
                throw new InvalidReservationStateException($"Cannot create payment for booking with ID {command.BookingId} because the reservation is not active.");
            }

            if (booking.Reservation.EventSeat.Status != EventSeatStatus.Reserved)
            {
                throw new InvalidReservationStateException($"Cannot create payment for booking with ID {command.BookingId} because the event seat is not reserved.");
            }

            if (await _paymentRepository.HasPendingPaymentAsync(command.BookingId, cancellationToken))
            {
                throw new PaymentAlreadyPendingException($"A pending payment already exists for booking with ID {command.BookingId}.");
            }

            var payment = new Payment
            {
                BookingId = command.BookingId,
                Amount = booking.TotalPrice,
                Status = PaymentStatus.Pending,
                Provider = command.Provider,
                CreatedAt = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment, cancellationToken);

            try 
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (UniqueConstraintException ex) when (ex.ConstraintType == UniqueConstraintType.PendingPaymentPerBooking)
            {
                throw new PaymentAlreadyPendingException($"A pending payment already exists for booking {command.BookingId}.", ex);
            }

            return new CreatePaymentResponse
            {
                Id = payment.Id,
                BookingId = payment.BookingId,
                Amount = payment.Amount,
                Status = payment.Status,
                Provider = payment.Provider,
                CreatedAt = payment.CreatedAt
            };
        }
    }
}