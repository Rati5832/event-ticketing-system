using EventTicketingSystem.Application.Common.Validations;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Seats.GetSeatById
{
    public class GetSeatByIdValidator : AbstractValidator<GetSeatByIdRequest>
    {
        public GetSeatByIdValidator()
        {
            RuleFor(x => x.Id).MustHaveValidId();
        }
    }
}
