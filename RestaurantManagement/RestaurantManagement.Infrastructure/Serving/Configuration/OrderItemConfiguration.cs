using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Serving.Models;
using static RestaurantManagement.Domain.Common.ModelConstants.StringConstants;

namespace RestaurantManagement.Infrastructure.Serving.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
                .HasKey(oi => oi.Id);

            builder
                .Property(oi => oi.Note)
                .IsRequired()
                .HasMaxLength(MaxDefaultStringLenght);

            builder
                .HasOne(oi => oi.Dish)
                .WithMany()
                .HasForeignKey("DishId");
        }
    }
}
