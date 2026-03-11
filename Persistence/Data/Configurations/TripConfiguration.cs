using Domain.Modules.TripModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Price)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.bus)
                   .WithMany(b => b.Trips)
                   .HasForeignKey(t => t.busId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.tickets)
                   .WithOne(tk => tk.trip)
                   .HasForeignKey(tk => tk.tripId);
            builder.HasIndex(t => new
            {
                t.busId,
                t.DateOfDeparture,
                
                
            })
            .IsUnique();

        }
    }
}
