namespace EventTicketingSystem.Application.Features.Reservations.CreateReservation
{
    public class CreateReservationCommand
    {
        public int EventSeatId { get; set; }

        public int UserId { get; set; }
    }
}
