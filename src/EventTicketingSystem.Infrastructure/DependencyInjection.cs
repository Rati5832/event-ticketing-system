using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Infrastructure.Persistence;
using EventTicketingSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventTicketingSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string not found.");
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<IEventSeatRepository, EventSeatRepository>();

            services.AddScoped<IUnitOfWork>(provider =>
                provider.GetRequiredService<ApplicationDbContext>()
            );

            return services;
        }
    }
}
