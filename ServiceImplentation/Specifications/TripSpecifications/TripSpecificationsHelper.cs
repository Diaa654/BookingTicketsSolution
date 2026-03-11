using Domain.Modules.TripModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.TripSpecifications
{
    internal static class TripSpecificationsHelper
    {
       
        public static Expression<Func<Trip, bool>> GetTripCriteria(TripQueryParams queryParams)
        {
            return t =>
             (string.IsNullOrEmpty(queryParams.ToCity) || t.CityTrips.Any(ct => ct.ToCity.Name.ToLower().Contains(queryParams.ToCity.ToLower()))) &&
            (string.IsNullOrEmpty(queryParams.FromCity) || t.CityTrips.Any(ct => ct.FromCity.Name.ToLower().Contains(queryParams.FromCity.ToLower()))) &&
            (!queryParams.Date.HasValue || t.DateOfDeparture == queryParams.Date.Value.Date);
        }
        public static Expression<Func<Trip, bool>> GetTripCriteriaWithBusId(TripQueryParams queryParams,int BusId)
        {
            return t =>(t.busId==BusId) &&
             (string.IsNullOrEmpty(queryParams.ToCity) || t.CityTrips.Any(ct => ct.ToCity.Name.ToLower().Contains(queryParams.ToCity.ToLower()))) &&
            (string.IsNullOrEmpty(queryParams.FromCity) || t.CityTrips.Any(ct => ct.FromCity.Name.ToLower().Contains(queryParams.FromCity.ToLower()))) &&
            (!queryParams.Date.HasValue ||(
     t.DateOfDeparture >= queryParams.Date.Value.Date &&
     t.DateOfDeparture < queryParams.Date.Value.Date.AddDays(1)));
        }
    }
}
