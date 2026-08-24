using EventTicketingSystem.Application.Features.Seats.CreateSeat;
using EventTicketingSystem.Application.Features.Venues.CreateVenue;
using EventTicketingSystem.Application.Features.Venues.GetVenueById;
using EventTicketingSystem.Application.Features.Venues.GetVenues;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly CreateVenueCommandHandler _createVenueHandler;
        private readonly GetVenueByIdHandler _getVenueByIdHandler;
        private readonly GetVenuesHandler _getVenuesHandler;
        private readonly CreateSeatCommandHandler _createSeatHandler;

        public VenuesController(CreateVenueCommandHandler createVenueHandler, 
            GetVenueByIdHandler getVenueByIdHandler,
            GetVenuesHandler getVenuesHandler,
            CreateSeatCommandHandler createSeatHandler)
        {
            _createVenueHandler = createVenueHandler;
            _getVenueByIdHandler = getVenueByIdHandler;
            _getVenuesHandler = getVenuesHandler;
            _createSeatHandler = createSeatHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var response = await _createVenueHandler.HandleAsync(request, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPost("{venueId:int}/seats")]
        public async Task<IActionResult> Create(int venueId, CreateSeatRequest createSeatRequest, CancellationToken cancellationToken)
        {
            var seatDto = await _createSeatHandler.HandleAsync(venueId, createSeatRequest, cancellationToken);

            return StatusCode(201, seatDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] GetVenueByIdRequest venueRequest, CancellationToken cancellationToken)
        {
            var response = await _getVenueByIdHandler.HandleAsync(venueRequest, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _getVenuesHandler.HandleAsync(cancellationToken);
            return Ok(response);
        }
    }
}
