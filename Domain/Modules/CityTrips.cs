using Domain.Modules.CityModule;
using Domain.Modules.TripModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules
{
    public class CityTrips
    {
        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public int TripId { get; set; }
        public City FromCity { get; set; } = default!;
        public City ToCity { get; set; } = default!;
        public Trip Trip { get; set; } = default!;
        public DateTime DateOfDeparture { get; set; }

    }
}
