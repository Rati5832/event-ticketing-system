using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Application.Abstractions.Persistence
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking, CancellationToken cancellationToken);

        Task<bool> ExistsByReservationIdAsync(int reservationId, CancellationToken cancellationToken);

        Task<Booking?> GetBookByIdAsync(int bookId, CancellationToken cancellationToken);

        Task<IEnumerable<Booking>> GetBooksAsync(CancellationToken cancellationToken);
    }
}
