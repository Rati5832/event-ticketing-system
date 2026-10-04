using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Domain.Enums;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Payments.ProcessPayment
{
    public class ProcessPaymentCommandHandler
    {
        private readonly IValidator<ProcessPaymentCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;

        public ProcessPaymentCommandHandler(IValidator<ProcessPaymentCommand> validator, IUnitOfWork unitOfWork, IPaymentRepository paymentRepository)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
        }

        public async Task<ProcessPaymentResponse> HandleAsync(int paymentId, ProcessPaymentRequest request, CancellationToken cancellationToken = default)
        {
            var command = new ProcessPaymentCommand
            {
                PaymentId = paymentId,
                IsSuccessful = request.IsSuccessful
            };

            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var payment = await _paymentRepository.GetByIdAsync(command.PaymentId, cancellationToken);

            if (payment == null)
            {
                throw new NotFoundException($"Payment with ID {command.PaymentId} not found.");
            }

            if (payment.Booking.Status != BookingStatus.Pending)
            {
                throw new InvalidBookingException(
                    $"Booking with ID {payment.Booking.Id} is not pending.");
            }

            if (payment.Status != PaymentStatus.Pending)
            {
                throw new InvalidPaymentStateException($"Payment with ID {command.PaymentId} is not in a pending state.");
            }


            if (command.IsSuccessful)
            {
                payment.Status = PaymentStatus.Succeeded;
                payment.Booking.Status = BookingStatus.Confirmed;
            }
            else
            {
                payment.Status = PaymentStatus.Failed;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new ProcessPaymentResponse
            {
                PaymentId = payment.Id,
                BookingId = payment.Booking.Id,
                PaymentStatus = payment.Status,
                BookingStatus = payment.Booking.Status,
            };
        }
    }
}
