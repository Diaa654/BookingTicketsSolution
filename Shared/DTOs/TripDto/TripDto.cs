using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.TripDto
{
    public class TripDto
    {
        public int Id { get; set; }
        public string FromCityName { get; set; } = default!;
        public string ToCityName { get; set; } = default!;

        public DateTime DateOfDeparture { get; set; }
        public decimal Duration { get; set; }
        public string BusNumber { get; set; }=default!;

        public decimal Price { get; set; }

        public int TotalSeats { get; set; }
        public int BookedSeats { get; set; }
        public int AvailableSeats => TotalSeats - BookedSeats;
        public bool IsActive { get; set; }
        public string DriverName { get; set; }=default!;
    }

}
