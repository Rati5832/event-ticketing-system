namespace EventTicketingSystem.Application.Features.Venues.CreateVenue
{
    public class CreateVenueCommand
    {
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
