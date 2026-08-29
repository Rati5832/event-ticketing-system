using EventTicketingSystem.Application.Features.Events.AssignSeatToEvent;
using EventTicketingSystem.Application.Features.Events.CreateEvent;
using EventTicketingSystem.Application.Features.Events.GetEventById;
using EventTicketingSystem.Application.Features.Events.GetEvents;
using EventTicketingSystem.Application.Features.Events.GetSeatsByEvent;
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
        private readonly AssignSeatToEventHandler _assignSeatToEventHandler;
        private readonly GetSeatsByEventHandler _getSeatsByEventHandler;

        public EventsController(
            CreateEventCommandHandler createEventHandler,
            GetEventByIdHandler getEventByIdHandler,
            GetEventsHandler getEventsHandler,
            AssignSeatToEventHandler assignSeatToEventHandler,
            GetSeatsByEventHandler getSeatsByEventHandler
            )
        {
            _createEventHandler = createEventHandler;
            _getEventByIdHandler = getEventByIdHandler;
            _getEventsHandler = getEventsHandler;
            _assignSeatToEventHandler = assignSeatToEventHandler;
            _getSeatsByEventHandler = getSeatsByEventHandler;
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

        [HttpPost("{eventId:int}/seats")]
        public async Task<IActionResult> AssignSeatToEvent(int eventId, AssignSeatToEventRequest seatToEventRequest, CancellationToken cancellationToken)
        {

            var assignSeatToEventDto = await _assignSeatToEventHandler.HandleAsync(eventId, seatToEventRequest, cancellationToken);
            return StatusCode(201, assignSeatToEventDto);
        }

        [HttpGet("{Id:int}/seats")]
        public async Task<IActionResult> GetAllSeatsByEvent([FromRoute] GetEventByIdRequest eventId, CancellationToken cancellationToken)
        {
            return Ok(await _getSeatsByEventHandler.HandleAsync(eventId, cancellationToken));
        }
    }
}
