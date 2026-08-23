using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTicketingSystem.Infrastructure.Persistence.Configurations
{
    public class EventSeatConfiguration : IEntityTypeConfiguration<EventSeat>
    {
        public void Configure(EntityTypeBuilder<EventSeat> builder)
        {
            builder.HasKey(es => es.Id);
            builder.Property(es => es.Price)
                .IsRequired()
                .HasPrecision(18, 2);
            builder.Property(es => es.Status)
                .IsRequired();
            builder.HasIndex(es => new { es.EventId, es.SeatId }).IsUnique();
        }
    }
}
