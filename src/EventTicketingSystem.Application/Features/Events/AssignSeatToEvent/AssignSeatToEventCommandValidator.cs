using EventTicketingSystem.Application.Common.Validations;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Events.AssignSeatToEvent
{
    public class AssignSeatToEventCommandValidator : AbstractValidator<AssignSeatToEventCommand>
    {
        public AssignSeatToEventCommandValidator()
        {
            RuleFor(x => x.EventId)
                .MustHaveValidId();

            RuleFor(x => x.SeatId)
                .MustHaveValidId();

            RuleFor(x => x.Price)
                .MustHaveValidId();
        }
    }
}
