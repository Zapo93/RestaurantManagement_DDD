using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static RestaurantManagement.Common.Domain.ModelConstants.StringConstants;

namespace RestaurantManagement.Infrastructure.Kitchen.Configuration
{
    internal class RequestItemConfiguration : IEntityTypeConfiguration<RequestItem>
    {
        public void Configure(EntityTypeBuilder<RequestItem> builder)
        {
                builder
                    .HasKey(ri => ri.Id);

                builder
                   .Property(i => i.Note)
                   .HasMaxLength(MaxDefaultStringLenght);

            builder
                .OwnsOne(
                    r => r.Status,
                    s => {
                        //s.WithOwner(); 
                        //s.Property(sr => sr.Name);
                        s.Property(sr => sr.Value);
                    });

            builder
                    .HasOne(r => r.Recipe)
                    .WithMany()
                    .HasForeignKey("RecipeId");
        }
    }
}
