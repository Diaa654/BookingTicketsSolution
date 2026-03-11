using Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class CityTripsConfigurations :IEntityTypeConfiguration<CityTrips>
    {
        public void Configure(EntityTypeBuilder<CityTrips> builder)
        {
            builder.HasKey(ct => new { ct.FromCityId, ct.ToCityId, ct.TripId });
            builder.HasOne(ct => ct.FromCity)
                   .WithMany(c => c.FromCityTrips)
                   .HasForeignKey(ct => ct.FromCityId)
                   .OnDelete(DeleteBehavior.Restrict);
           builder.HasOne(ct => ct.ToCity)
                   .WithMany(c => c.ToCityTrips)
                   .HasForeignKey(ct => ct.ToCityId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ct => ct.Trip)
                .WithMany(t => t.CityTrips)
                .HasForeignKey(ct => ct.TripId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(ct => new
            {
                ct.FromCityId,
                ct.ToCityId,
                ct.DateOfDeparture
            }).IsUnique();

        }
    }
}
