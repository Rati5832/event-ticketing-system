using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Application.Abstractions.Persistence
{
    public interface IEventRepository
    {
        Task AddAsync(Event eventEntity, CancellationToken cancellation = default);
    }
}
