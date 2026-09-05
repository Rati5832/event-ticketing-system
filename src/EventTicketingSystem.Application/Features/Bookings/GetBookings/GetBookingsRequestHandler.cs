using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Features.Bookings.Common;

namespace EventTicketingSystem.Application.Features.Bookings.GetBookings
{
    public class GetBookingsRequestHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingsRequestHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<BookingResponse>> Handle(CancellationToken cancellationToken = default)
        {
            var bookings = await _bookingRepository.GetBooksAsync(cancellationToken);
            return bookings.Select(b => new BookingResponse
            {
                Id = b.Id,
                UserId = b.UserId,
                ReservationId = b.ReservationId,
                BookingNumber = b.BookingNumber,
                TotalPrice = b.TotalPrice,
                CreatedAt = b.CreatedAt,
                Status = b.Status,
            }).ToList();
        }
    }
}
