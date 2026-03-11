using Domain.Modules.UserModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.UserId);
            builder.HasMany(d => d.DriverTrips)
                   .WithOne(t => t.Driver)
                   .HasForeignKey(t => t.DriverId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(d => d.LicenseNumber).IsUnique();
        }
    }
}
