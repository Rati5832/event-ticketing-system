using FluentValidation;

namespace EventTicketingSystem.Application.Features.Reservations.CreateReservation
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(r => r.EventSeatId)
                .GreaterThan(0).WithMessage("EventSeatId must be greater than 0.");
        }
    }
}
