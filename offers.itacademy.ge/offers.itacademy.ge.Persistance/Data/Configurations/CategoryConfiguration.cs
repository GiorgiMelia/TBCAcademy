using ITAcademy.Offers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITAcademy.Offers.Persistence.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            builder.HasData(
        new Category { Id = 1, Name = "Food" },
        new Category { Id = 2, Name = "Drinks" },
        new Category { Id = 3, Name = "Plants" },
        new Category { Id = 4, Name = "Sweets" }
        );
        }
    }
}
