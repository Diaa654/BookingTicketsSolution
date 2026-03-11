using Domain.Modules.TicketModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Price)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.trip)
                   .WithMany(tr => tr.tickets)
                   .HasForeignKey(t => t.tripId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
