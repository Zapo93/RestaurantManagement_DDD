using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Serving.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static RestaurantManagement.Common.Domain.ModelConstants.StringConstants;

namespace RestaurantManagement.Infrastructure.Serving.Configuration
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(MaxDefaultStringLenght);

            builder
                .Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(MaxDefaultStringLenght);

            builder
                .Property(d => d.RecipeId)
                .IsRequired();

            builder
                .Property(d => d.Active)
                .IsRequired();

            builder
                .Property(d => d.ImageUrl);

            //TODO WHAT TO DO WITH PRICE?
            builder
                .OwnsOne(d => d.Price, p =>
                {
                    p.WithOwner();

                    p.Property(op => op.Value);
                    p.Property(op => op.CurrencyAbbreviation);
                });
        }
    }
}
