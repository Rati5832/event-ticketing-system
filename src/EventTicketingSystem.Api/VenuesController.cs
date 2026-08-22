using EventTicketingSystem.Application.Features.Venues.CreateVenue;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly CreateVenueHandler _createVenueHandler;

        public VenuesController(CreateVenueHandler createVenueHandler)
        {
            _createVenueHandler = createVenueHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVenueRequest request, CancellationToken cancellationToken)
        {
            var response = await _createVenueHandler.HandleAsync(request, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response);
        }
    }
}
