using EventTicketingSystem.Application.Common.Validations;
using EventTicketingSystem.Domain.Enums;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Payments.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.BookingId)
                .MustHaveValidId();
            RuleFor(x => x.Provider)
                .IsInEnum()
                .WithMessage("Invalid payment provider.")
                .NotEqual(PaymentProvider.Unknow)
                .WithMessage("A valid payment provider is required.");
        }
    }
}
