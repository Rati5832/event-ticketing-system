using EventTicketingSystem.Application.Features.Reservations.CreateReservation;
using EventTicketingSystem.Application.Features.Reservations.GetReservations;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly CreateReservationCommandHandler _createReservationCommandHandler;
        private readonly GetReservationsRequestHandler _getReservationsRequestHandler;

        public ReservationsController(
            CreateReservationCommandHandler createReservationCommandHandler,
            GetReservationsRequestHandler getReservationsRequestHandler)
        {
            _createReservationCommandHandler = createReservationCommandHandler;
            _getReservationsRequestHandler = getReservationsRequestHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateByEventSeat(CreateReservationCommand createReservation, CancellationToken cancellationToken)
        {
            var reservationDto = await _createReservationCommandHandler.Handle(createReservation, cancellationToken);
            return StatusCode(201, reservationDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var reservations = await _getReservationsRequestHandler.Handle(cancellationToken);
            return Ok(reservations);
        }

    }
}
