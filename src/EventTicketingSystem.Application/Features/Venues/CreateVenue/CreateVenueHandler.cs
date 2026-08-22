using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;
using FluentValidation;

namespace EventTicketingSystem.Application.Features.Venues.CreateVenue
{
    public class CreateVenueHandler
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateVenueRequest> _venueRequestValidation;

        public CreateVenueHandler(IVenueRepository venueRepository, IUnitOfWork unitOfWork, IValidator<CreateVenueRequest> _validator)
        {
            _venueRepository = venueRepository;
            _unitOfWork = unitOfWork;
            _venueRequestValidation = _validator;
        }

        public async Task<VenueResponse> HandleAsync(CreateVenueRequest request, CancellationToken cancellationToken = default)
        {
            var validation = await _venueRequestValidation.ValidateAsync(request, cancellationToken);

            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var venue = new Venue
            {
                Name = request.Name,
                Address = request.Address,
            };

            await _venueRepository.AddAsync(venue, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new VenueResponse
            {
                Id = venue.Id,
                Name = venue.Name,
                Address = venue.Address
            };
        }
    }
}
