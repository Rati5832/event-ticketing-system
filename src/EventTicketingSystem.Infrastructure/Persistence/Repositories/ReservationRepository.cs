using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Infrastructure.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reservation reservationEntity, CancellationToken cancellation = default)
        {
            await _context.AddAsync(reservationEntity, cancellation);
        }
    }
}
