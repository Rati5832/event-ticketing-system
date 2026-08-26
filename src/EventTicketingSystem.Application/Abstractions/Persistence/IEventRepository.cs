using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Application.Abstractions.Persistence
{
    public interface IEventRepository
    {
        Task AddAsync(Event eventEntity, CancellationToken cancellation = default);

        Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken);

        Task<Event?> GetByIdAsync(int id, CancellationToken cancellation = default);

    }
}
