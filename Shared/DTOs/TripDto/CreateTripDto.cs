using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.TripDto
{
    public class CreateTripDto
    {
        public string FromCityName { get; set; }=default!;
        public string ToCityName { get; set; } = default!;

        public DateTime DateOfDeparture { get; set; }
        public decimal Duration { get; set; }
        public string BusPlateNumber { get; set; }=default!;
        public decimal Price { get; set; }
        public string DriverName { get; set; }=default!;
    }

}
