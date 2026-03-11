using Domain.Modules.PaymentModule;
using Domain.Modules.TicketModule;
using Domain.Modules.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.BookingsModule
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalAmount { get; set; }
        public string Status { get; set; } = default!;
        public int PassengerId { get; set; }
        public Payment Payment { get; set; } = default!;
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        
    }
}
