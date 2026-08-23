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
        private readonly CreateVenueHandler _createVenueHandler;
        private readonly GetVenueByIdHandler _getVenueByIdHandler;
        private readonly GetVenuesHandler _getVenuesHandler;

        public VenuesController(CreateVenueHandler createVenueHandler, GetVenueByIdHandler getVenueByIdHandler, GetVenuesHandler getVenuesHandler)
        {
            _createVenueHandler = createVenueHandler;
            _getVenueByIdHandler = getVenueByIdHandler;
            _getVenuesHandler = getVenuesHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVenueRequest request, CancellationToken cancellationToken)
        {
            var response = await _createVenueHandler.HandleAsync(request, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response);
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
