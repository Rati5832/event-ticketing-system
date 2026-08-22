using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Venue> Venues => Set<Venue>();

        public DbSet<Event> Events => Set<Event>();

        public DbSet<Seat> Seats => Set<Seat>();

        public DbSet<EventSeat> EventSeats => Set<EventSeat>();

        public DbSet<Reservation> Reservations => Set<Reservation>();

        public DbSet<Booking> Bookings => Set<Booking>();

        public DbSet<Payment> Payments => Set<Payment>();
    }
}
