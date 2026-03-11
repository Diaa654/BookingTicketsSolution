using Domain.Modules.CityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.CitySpecifications.cs
{
    internal class CityByNameSpec: BaseSpecification<City, int>
    {
        public CityByNameSpec(string cityName) :
            base(c => c.Name == cityName)
        { 
        
        }
    }
}
