using EventTicketingSystem.Application.Features.Bookings.CreateBooking;
using EventTicketingSystem.Application.Features.Bookings.GetBookings;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly CreateBookingCommandHandler _createBookingCommandHandler;
        private readonly GetBookingsRequestHandler _getBookingsRequestHandler;

        public BookingsController(
            CreateBookingCommandHandler createBookingCommandHandler,
            GetBookingsRequestHandler getBookingsRequestHandler)
        {
            _createBookingCommandHandler = createBookingCommandHandler;
            _getBookingsRequestHandler = getBookingsRequestHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            var bookingResponse = await _createBookingCommandHandler.HandleAsync(command, cancellationToken);
            return StatusCode(201, bookingResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var bookings = await _getBookingsRequestHandler.Handle(cancellationToken);
            return Ok(bookings);
        }
    }
}
