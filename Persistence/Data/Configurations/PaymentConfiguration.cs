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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Currency)
                   .HasMaxLength(10);

            builder.Property(p => p.PaymentMethod)
                   .HasMaxLength(50);

            builder.Property(p => p.Provider)
                   .HasMaxLength(50);
        }
    }
}
