using ITAcademy.Offers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITAcademy.Offers.Persistence.Data.Configurations
{
    internal class BuyerEntityConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(100);
            builder.Property(b => b.Surname).HasMaxLength(100);
            builder.Property(b => b.Address).HasMaxLength(500);
            builder.Property(b => b.Balance).HasPrecision(18, 2);

        }
    }
}
