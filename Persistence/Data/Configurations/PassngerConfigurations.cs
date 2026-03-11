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
    public class PassengerConfigurations : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.HasMany(p=>p.Bookings)
                .WithOne()
                .HasForeignKey(b=>b.PassengerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
