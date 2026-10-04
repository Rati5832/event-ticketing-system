using EventTicketingSystem.Application.Common.Validations;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Payments.ProcessPayment
{
    public class ProcessPaymentCommandValidator : AbstractValidator<ProcessPaymentCommand>
    {
        public ProcessPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId)
                .MustHaveValidId();
        }
    }
}