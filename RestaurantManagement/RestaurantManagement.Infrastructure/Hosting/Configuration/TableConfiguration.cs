using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Hosting.Domain.Models;
using static RestaurantManagement.Common.Domain.ModelConstants.StringConstants;

namespace RestaurantManagement.Infrastructure.Hosting.Configuration
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(MaxDefaultStringLenght);

            builder
                .Property(t => t.RestaurantName)
                .HasMaxLength(MaxDefaultStringLenght);

            builder
               .Property(t => t.NumberOfSeats)
               .IsRequired();

            ///TODO Check!

            builder
                .OwnsOne(t => t.Description, o =>
                {
                    o.WithOwner();

                    o.Property(td => td.Location);
                    o.Property(td => td.AreSmokersAllowed);
                    o.Property(td => td.IsIndoor);
                });

            builder
                .HasMany(t => t.Schedules)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("schedules");
        }
    }
}
