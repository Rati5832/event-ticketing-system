using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Common.Exceptions.UniqueConstraint;
using EventTicketingSystem.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private const string PendingPaymentIndex = "IX_Payments_BookingId";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(
                    "The resource was modified by another operation.",
                    ex);
            }
            catch (DbUpdateException ex) when (
                ex.InnerException is SqlException sqlEx &&
                (sqlEx.Number == 2601 || sqlEx.Number == 2627) &&
                sqlEx.Message.Contains(
                    PendingPaymentIndex,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new UniqueConstraintException(UniqueConstraintType.PendingPaymentPerBooking, ex);
            }
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
