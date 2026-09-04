using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTicketingSystem.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.BookingNumber).IsRequired().HasMaxLength(30);
            builder.HasIndex(b => b.BookingNumber).IsUnique();
            builder.Property(b => b.TotalPrice).IsRequired().HasPrecision(18, 2);
            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.Status).IsRequired();

            builder
                .HasOne(b => b.Reservation)
                .WithOne(r => r.Booking)
                .HasForeignKey<Booking>(b => b.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(b => b.Payments)
                .WithOne(p => p.Booking)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
