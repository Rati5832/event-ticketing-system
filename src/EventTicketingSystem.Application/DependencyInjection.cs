using EventTicketingSystem.Application.Features.Events.CreateEvent;
using EventTicketingSystem.Application.Features.Events.GetEventById;
using EventTicketingSystem.Application.Features.Events.GetEvents;
using EventTicketingSystem.Application.Features.Venues.CreateVenue;
using EventTicketingSystem.Application.Features.Venues.GetVenueById;
using EventTicketingSystem.Application.Features.Venues.GetVenues;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EventTicketingSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVenueHandler>();
            services.AddScoped<GetVenueByIdHandler>();
            services.AddScoped<GetVenuesHandler>();

            services.AddScoped<CreateEventHandler>();
            services.AddScoped<GetEventByIdHandler>();
            services.AddScoped<GetEventsHandler>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}
