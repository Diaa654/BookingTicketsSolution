using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.CityModule
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<CityTrips> ToCityTrips { get; set; } = new List<CityTrips>();
        public ICollection<CityTrips> FromCityTrips { get; set; } = new List<CityTrips>();
    }
}
