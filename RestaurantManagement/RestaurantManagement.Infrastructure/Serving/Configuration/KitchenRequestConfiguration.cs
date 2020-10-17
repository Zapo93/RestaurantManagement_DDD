using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure.Serving.Configuration
{
    public class KitchenRequestConfiguration : IEntityTypeConfiguration<KitchenRequest>
    {
        public void Configure(EntityTypeBuilder<KitchenRequest> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(k => k.RequestId);
        }
    }
}
