using EventTicketingSystem.Application.Features.Reservations.CreateReservation;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly CreateReservationCommandHandler _createReservationCommandHandler;

        public ReservationsController(CreateReservationCommandHandler createReservationCommandHandler)
        {
            _createReservationCommandHandler = createReservationCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateByEventSeat(CreateReservationCommand createReservation, CancellationToken cancellationToken)
        {
            var reservationDto = await _createReservationCommandHandler.Handle(createReservation, cancellationToken);
            return StatusCode(201, reservationDto);
        }
    }
}
