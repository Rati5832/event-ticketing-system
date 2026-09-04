using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Enums;

namespace EventTicketingSystem.Application.Features.Reservations.ExpiredReservations
{
    public class ExpireReservationHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExpireReservationHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(int reservationId, DateTime currentTime, CancellationToken cancellationToken = default)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId, cancellationToken);

            if (reservation == null)
            {
                return;
            }

            if (reservation.Status != ReservationStatus.Active)
            {
                return;
            }

            if (reservation.ExpiresAt >= currentTime)
            {
                return;
            }

            if (reservation.EventSeat.Status != EventSeatStatus.Reserved)
            {
                return;
            }

            reservation.Status = ReservationStatus.Expired;
            reservation.EventSeat.Status = EventSeatStatus.Available;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
