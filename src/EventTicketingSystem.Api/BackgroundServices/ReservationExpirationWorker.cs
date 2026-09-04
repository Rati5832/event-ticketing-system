using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Features.Reservations.ExpiredReservations;

namespace EventTicketingSystem.Api.BackgroundServices
{
    public class ReservationExpirationWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ReservationExpirationWorker> _logger;

        public ReservationExpirationWorker(IServiceScopeFactory scopeFactory, ILogger<ReservationExpirationWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var reservationRepository = scope.ServiceProvider.GetRequiredService<IReservationRepository>();

                var currentTime = DateTime.UtcNow;
                var reservationIds = await reservationRepository.GetExpiredReservationIdsAsync(currentTime, stoppingToken);

                foreach (var reservationId in reservationIds)
                {
                    using var innerScope = _scopeFactory.CreateScope();
                    var reservationHandler = innerScope.ServiceProvider.GetRequiredService<ExpireReservationHandler>();
                    try
                    {
                        await reservationHandler.HandleAsync(reservationId, currentTime, stoppingToken);
                    }
                    catch (ConcurrencyException ex)
                    {
                        _logger.LogWarning(ex,
                            "Concurrency conflict while expiring reservation {ReservationId}",
                            reservationId);
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
            }
        }
    }
}
