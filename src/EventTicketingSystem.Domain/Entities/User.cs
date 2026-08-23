namespace EventTicketingSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
