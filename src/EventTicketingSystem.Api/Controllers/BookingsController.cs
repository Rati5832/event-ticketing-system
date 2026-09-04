using EventTicketingSystem.Application.Features.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly CreateBookingCommandHandler _createBookingCommandHandler;

        public BookingsController(CreateBookingCommandHandler createBookingCommandHandler)
        {
            _createBookingCommandHandler = createBookingCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            var bookingResponse = await _createBookingCommandHandler.HandleAsync(command, cancellationToken);
            return StatusCode(201, bookingResponse);
        }
    }
}
