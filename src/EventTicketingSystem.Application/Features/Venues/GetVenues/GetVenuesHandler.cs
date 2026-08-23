using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Features.Venues.Common;

namespace EventTicketingSystem.Application.Features.Venues.GetVenues
{
    public class GetVenuesHandler
    {
        private readonly IVenueRepository _venueRepository;

        public GetVenuesHandler(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public async Task<List<VenueResponse>> HandleAsync(CancellationToken cancellationToken)
        {
            var venueList = await _venueRepository.GetAllAsync(cancellationToken);

            return venueList.Select(venue => new VenueResponse
            {
                Id = venue.Id,
                Name = venue.Name,
                Address = venue.Address,
            }).ToList();
        }
    }
}
