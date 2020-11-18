using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Hosting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Hosting.Infrastructure.Configuration
{
    internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder
                .OwnsOne(r => r.TimeRange, o =>
                {
                    o.WithOwner();
                    
                    o.Property(tr => tr.Start);
                    o.Property(tr => tr.End);
                });

            builder
                .OwnsOne(r => r.Guest, o =>
                {
                    o.WithOwner();

                    o.Property(g => g.FirstName);
                    o.Property(g => g.LastName); 
                    o.Property(g => g.PhoneNumber);
                });
        }
    }
}
