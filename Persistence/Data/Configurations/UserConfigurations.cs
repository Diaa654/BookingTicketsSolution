using Domain.Modules.BookingsModule;
using Domain.Modules.UserModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           builder.HasKey(u => u.Id);
           builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(50);
           builder.Property(u => u.LastName).HasMaxLength(50);
          builder.HasOne<Passenger>()
               .WithOne(p => p.User)
               .HasForeignKey<Passenger>(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);
           builder.HasOne<Driver>()
               .WithOne(d => d.User)
               .HasForeignKey<Driver>(d => d.UserId)
               .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Users");
        }

        
    }
}
