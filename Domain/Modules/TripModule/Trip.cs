using Domain.Modules.BusModule;
using Domain.Modules.TicketModule;
using Domain.Modules.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.TripModule
{
    public class Trip
    {
        public int Id { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public ICollection<Ticket> tickets { get; set; } = new List<Ticket>();
        public Bus bus { get; set; } = default!;
        public int busId { get; set; }
        public ICollection<CityTrips> CityTrips { get; set; } = new List<CityTrips>();
        public bool IsActive { get; set; } = true;
        public int BookedSeats { get; set; }
        public int AvailableSeats => bus.Capacity - BookedSeats;
        public int DriverId { get; set; }
        public Driver Driver { get; set; }=default!;
    }
}
