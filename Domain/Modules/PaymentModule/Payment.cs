using Domain.Modules.BookingsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.PaymentModule
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Provider { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string? Currency { get; set; } = default!;
        public string Status { get; set; } = default!;
        public Booking Booking { get; set; } = default!;
        public int BookingId { get; set; }
    }
}
