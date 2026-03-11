using Domain.Modules.BusModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Services.Specifications.BusSpecifications
{
    internal class BusTripsSpecifications:BaseSpecification<Bus,int>
    {
        public BusTripsSpecifications(string PlateNumber) :base(b=>b.PlateNumber==PlateNumber)
        {
            //AddInclude(b => b.Trips);
            //AddInclude(q => q.
            //Include(b => b.Trips)
            //.ThenInclude(t => t.CityTrips)
            //.ThenInclude(ct => ct.FromCity)
            //.Include(b => b.Trips).ThenInclude(t => t.CityTrips).ThenInclude(ct => ct.ToCity));
        }
    }
}
