using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Common.Exceptions;
using EventTicketingSystem.Application.Features.Venues.Common;

namespace EventTicketingSystem.Application.Features.Venues.GetVenueById
{
    public class GetVenueByIdHandler
    {
        private readonly IVenueRepository _venueRepository;

        public GetVenueByIdHandler(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public async Task<VenueResponse> HandleAsync(GetVenueByIdRequest request, CancellationToken cancellation = default)
        {
            var venue = await _venueRepository.GetByIdAsync(request.Id, cancellation);

            if (venue == null)
            {
                throw new NotFoundException($"Venue with id {request.Id} Not Found");
            }

            return new VenueResponse
            {
                Id = venue.Id,
                Name = venue.Name,
                Address = venue.Address
            };

        }
    }
}
