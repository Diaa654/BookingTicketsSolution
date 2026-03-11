using Domain.Modules.BookingsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.UserModule
{
    public class Passenger
    {
        public int UserId { get; set; }//Fk
        public User User { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Booking> Bookings { get; set; } = [];


    }
}
