using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Features.Reservations.Common;

namespace EventTicketingSystem.Application.Features.Reservations.GetReservations
{
    public class GetReservationsRequestHandler
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationsRequestHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<ReservationResponse>> Handle(CancellationToken cancellationToken = default)
        {
            var reservations = await _reservationRepository.GetAllAsync(cancellationToken);
            return reservations.Select(r => new ReservationResponse
            {
                ReservationId = r.Id,
                EventSeatId = r.EventSeatId,
                ExpiresAt = r.ExpiresAt,
                Status = r.Status
            }).ToList();
        }
    }
}
