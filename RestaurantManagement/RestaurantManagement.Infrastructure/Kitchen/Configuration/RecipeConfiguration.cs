using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static RestaurantManagement.Common.Domain.ModelConstants.StringConstants;
namespace RestaurantManagement.Infrastructure.Kitchen.Configuration
{
    internal class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder
                .HasKey(r=> r.Id);

            builder
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(MaxDefaultStringLenght);

            builder
                .Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(MaxDefaultStringLenght);

            builder
                .Property(r => r.Preparation)
                .HasMaxLength(MaxDefaultLongStringLenght);

            builder
                .Property(r => r.Active)
                .IsRequired();

            builder
                .HasMany(r => r.Ingredients)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("ingredients");
        }
    }
}
