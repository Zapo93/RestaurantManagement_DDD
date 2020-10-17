﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure.Kitchen.Configuration
{
    internal class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder) 
        {
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.DateCreated)
                .IsRequired();

            builder
                .Property(r => r.CreatorReferenceId)
                .IsRequired();

            builder
                .OwnsOne(
                    r => r.Status,
                    s => {
                        s.WithOwner(); 
                        s.Property(sr => sr.Value); 
                    });

            builder
                .HasMany(r => r.Items)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("items");
        }
    }
}