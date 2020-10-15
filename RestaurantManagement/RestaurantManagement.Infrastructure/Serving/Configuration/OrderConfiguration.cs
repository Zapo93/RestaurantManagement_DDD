using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure.Serving.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.TableId);

            builder
                .Property(o => o.AssigneeId)
                .IsRequired();

            builder
                .Property(o => o.DateCreated)
                .IsRequired();

            builder
                .Property(o => o.Open)
                .IsRequired();

            //TODO KitchenRequestIds and OrderItems
            builder
                .HasMany(o => o.Items)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("items");

            builder
                .HasMany(o => o.KitchenRequestIds)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("kitchenRequestIds");
        }
    }
}
