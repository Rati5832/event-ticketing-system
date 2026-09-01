using EventTicketingSystem.Application.Features.Events.AssignSeatToEvent;
using EventTicketingSystem.Application.Features.Events.CreateEvent;
using EventTicketingSystem.Application.Features.Events.GetEventById;
using EventTicketingSystem.Application.Features.Events.GetEvents;
using EventTicketingSystem.Application.Features.Events.GetSeatsByEvent;
using EventTicketingSystem.Application.Features.Reservations.CreateReservation;
using EventTicketingSystem.Application.Features.Seats.CreateSeat;
using EventTicketingSystem.Application.Features.Seats.GetSeatsByVenue;
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
            services.AddScoped<CreateVenueCommandHandler>();
            services.AddScoped<GetVenueByIdHandler>();
            services.AddScoped<GetVenuesHandler>();

            services.AddScoped<CreateEventCommandHandler>();
            services.AddScoped<GetEventByIdHandler>();
            services.AddScoped<GetEventsHandler>();

            services.AddScoped<CreateSeatCommandHandler>();
            services.AddScoped<GetSeatsByVenueHandler>();

            services.AddScoped<AssignSeatToEventHandler>();
            services.AddScoped<GetSeatsByEventHandler>();

            services.AddScoped<CreateReservationCommandHandler>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}
