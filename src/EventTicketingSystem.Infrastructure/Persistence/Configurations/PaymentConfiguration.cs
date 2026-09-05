using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventTicketingSystem.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Provider)
                .IsRequired();

            builder.Property(x => x.TransactionId).HasMaxLength(30);

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
