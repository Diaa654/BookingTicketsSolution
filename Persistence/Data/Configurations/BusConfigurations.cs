using Domain.Modules.BusModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class BusConfigurations : IEntityTypeConfiguration<Bus>
    {
        public void Configure(EntityTypeBuilder<Bus> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.PlateNumber)
                   .IsRequired();

            builder.Property(b => b.Capacity)
                   .IsRequired();

            builder.HasMany(b => b.Trips)
                   .WithOne(t => t.bus)
                   .HasForeignKey(t => t.busId);
        }
    }
}
