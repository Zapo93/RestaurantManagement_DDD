using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Serving.Domain.Models;
using static RestaurantManagement.Common.Domain.ModelConstants.StringConstants;

namespace RestaurantManagement.Serving.Infrastructure.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
                .HasKey(oi => oi.Id);

            builder
                .Property(oi => oi.Note)
                .HasMaxLength(MaxDefaultStringLenght);

            builder
                .HasOne(oi => oi.Dish)
                .WithMany()
                .HasForeignKey("DishId");
        }
    }
}
