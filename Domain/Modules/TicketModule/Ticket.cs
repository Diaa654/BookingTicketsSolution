using Domain.Modules.BookingsModule;
using Domain.Modules.TripModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.TicketModule
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Status { get; set; } = default!;
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }
        public int bookingId { get; set; }
        public Booking booking { get; set; }= default!;
        public Trip trip { get; set; } = default!;
        public int tripId { get; set; }
    }
}
