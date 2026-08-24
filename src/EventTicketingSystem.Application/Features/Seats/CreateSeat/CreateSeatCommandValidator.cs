using EventTicketingSystem.Application.Common.Validations;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Seats.CreateSeat
{
    public class CreateSeatCommandValidator : AbstractValidator<CreateSeatCommand>
    {
        public CreateSeatCommandValidator()
        {
            RuleFor(s => s.VenueId)
                .MustHaveValidId();
            RuleFor(s => s.Section)
                .NotEmpty().WithMessage("Section is required.")
                .MaximumLength(50).WithMessage("Section must not exceed 50 characters.");
            RuleFor(s => s.Row)
                .NotEmpty().WithMessage("Row is required.")
                .MaximumLength(50).WithMessage("Row must not exceed 50 characters.");
            RuleFor(s => s.Number)
                .MustHaveValidId();
        }
    }
}
