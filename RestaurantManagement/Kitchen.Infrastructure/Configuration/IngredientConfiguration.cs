using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Kitchen.Domain.Models;
using static RestaurantManagement.Common.Domain.ModelConstants.StringConstants;

namespace RestaurantManagement.Kitchen.Infrastructure.Configuration
{
    internal class IngredientConfigurationinternal : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            const string id = "Id";

            builder
                .Property<int>(id);

            builder
                .HasKey(id);

            builder
               .Property(i => i.Name)
               .IsRequired()
               .HasMaxLength(MaxDefaultStringLenght);

            builder
               .Property(i => i.QuantityInGrams)
               .IsRequired();
        }
    }
}
