using FluentValidation;

namespace EventTicketingSystem.Application.Features.Venues.CreateVenue
{
    public class CreateVenueCommandValidator : AbstractValidator<CreateVenueCommand>
    {
        public CreateVenueCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Venue name is required.")
                .MaximumLength(50).WithMessage("Venue name cannot exceed 50 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Venue address is required.")
                .MaximumLength(100).WithMessage("Venue address cannot exceed 100 characters.");
        }
    }
}
