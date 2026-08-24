using EventTicketingSystem.Application.Features.Events.CreateEvent;
using EventTicketingSystem.Application.Features.Events.GetEventById;
using EventTicketingSystem.Application.Features.Events.GetEvents;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly CreateEventCommandHandler _createEventHandler;
        private readonly GetEventByIdHandler _getEventByIdHandler;
        private readonly GetEventsHandler _getEventsHandler;

        public EventsController(CreateEventCommandHandler createEventHandler, GetEventByIdHandler getEventByIdHandler, GetEventsHandler getEventsHandler)
        {
            _createEventHandler = createEventHandler;
            _getEventByIdHandler = getEventByIdHandler;
            _getEventsHandler = getEventsHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventDto = await _createEventHandler.HandleAsync(request, cancellationToken);

            return StatusCode(201, eventDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] GetEventByIdRequest request, CancellationToken cancellationToken)
        {
            var eventDto = await _getEventByIdHandler.HandleAsync(request, cancellationToken);

            return Ok(eventDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var events = await _getEventsHandler.HandleAsync(cancellationToken);

            return Ok(events);
        }
    }
}
