using EventTicketingSystem.Application.Common.Validations;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Events.GetEventById
{
    public class GetEventByIdValidator : AbstractValidator<GetEventByIdRequest>
    {
        public GetEventByIdValidator()
        {
            RuleFor(x => x.Id).MustHaveValidId();
        }
    }
}
