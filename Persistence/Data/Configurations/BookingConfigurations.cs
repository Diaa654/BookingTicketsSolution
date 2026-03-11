using Domain.Modules.BookingsModule;
using Domain.Modules.PaymentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class BookingConfigurations : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {

            builder.HasKey(b => b.Id);

            builder.Property(b => b.TotalAmount)
                   .HasColumnType("decimal(18,2)");

            builder.HasMany(b => b.Tickets)
                   .WithOne(t => t.booking)
                   .HasForeignKey(t => t.bookingId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Payment)
                   .WithOne(p => p.Booking)
                   .HasForeignKey<Payment>(p => p.BookingId);


        }
    }
}
