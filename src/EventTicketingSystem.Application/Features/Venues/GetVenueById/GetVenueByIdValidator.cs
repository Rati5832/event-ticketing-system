using EventTicketingSystem.Application.Common.Validations;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Venues.GetVenueById
{
    public class GetVenueByIdValidator : AbstractValidator<GetVenueByIdRequest>
    {
        public GetVenueByIdValidator()
        {
            RuleFor(x => x.Id).MustHaveValidId();
        }
    }
}
