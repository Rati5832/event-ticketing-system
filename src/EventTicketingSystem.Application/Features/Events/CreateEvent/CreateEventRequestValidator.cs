
using EventTicketingSystem.Application.Common.Validations;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Events.CreateEvent
{
    public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
    {
        public CreateEventRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Event name is required.")
                .MaximumLength(100).WithMessage("Event name cannot exceed 100 characters.");

            RuleFor(x => x.VenueId)
                .MustHaveValidId();

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("Start date must be earlier than end date.");
        }
    }
}
