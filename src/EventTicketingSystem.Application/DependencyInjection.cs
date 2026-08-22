using EventTicketingSystem.Application.Features.Venues.CreateVenue;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EventTicketingSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVenueHandler>();
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}
