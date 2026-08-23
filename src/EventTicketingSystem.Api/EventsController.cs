using EventTicketingSystem.Application.Features.Events.CreateEvent;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly CreateEventHandler _createEventHandler;

        public EventsController(CreateEventHandler createEventHandler)
        {
            _createEventHandler = createEventHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventRequest request, CancellationToken cancellationToken)
        {
            var eventDto = await _createEventHandler.HandleAsync(request, cancellationToken);

            return StatusCode(201, eventDto);
        }
    }
}
