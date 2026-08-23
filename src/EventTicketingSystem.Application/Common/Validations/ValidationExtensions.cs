using FluentValidation;

namespace EventTicketingSystem.Application.Common.Validations
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, int> MustHaveValidId<T>( this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThanOrEqualTo(1)
                .WithMessage("ID must be a positive number");
        }
    }
}
