using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Hosting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure.Hosting.Configuration
{
    internal class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .OwnsOne(s => s.TimeRange, o =>
                {
                    o.WithOwner();

                    o.Property(tr => tr.Start);
                    o.Property(tr => tr.End);
                });

            builder
                .HasMany(s => s.Reservations)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("reservations");
        }
    }
}
