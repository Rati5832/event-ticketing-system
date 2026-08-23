using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTicketingSystem.Infrastructure.Persistence.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Section).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Row).IsRequired().HasMaxLength(50);

            builder
                .HasMany(s => s.EventSeats)
                .WithOne(e => e.Seat)
                .HasForeignKey(e => e.SeatId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
