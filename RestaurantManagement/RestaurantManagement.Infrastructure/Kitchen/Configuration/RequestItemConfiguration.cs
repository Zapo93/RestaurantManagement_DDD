using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static RestaurantManagement.Domain.Common.ModelConstants.StringConstants;

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
                   .IsRequired()
                   .HasMaxLength(MaxDefaultStringLenght);

                builder
                    .OwnsOne(
                        r => r.Status,
                        s => {
                            s.WithOwner();
                            s.Property(sr => sr.Value);
                        });

                builder
                    .HasOne(r => r.Recipe)
                    .WithMany()
                    .HasForeignKey("RecipeId");
        }
    }
}
