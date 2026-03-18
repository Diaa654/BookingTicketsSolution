using Domain.Modules.TripModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;


namespace Services.Specifications.TripSpecifications
{
    internal class TripWithCItysSpecification:BaseSpecification<Trip,int>
    {
        public TripWithCItysSpecification(int id):base(t=>t.Id==id)
        {
            AddInclude(t => t.bus);
            AddInclude(q => q.Include(t => t.Driver).ThenInclude(d => d.User));
            //Include متداخل(ThenInclude)
            AddInclude(q =>
                q.Include(t => t.CityTrips)
                 .ThenInclude(ct => ct.FromCity)
                 .Include(t => t.CityTrips)
                 .ThenInclude(ct => ct.ToCity));

            AddInclude(t => t.Driver);
            
        }
        public TripWithCItysSpecification(TripQueryParams queryParams):
            base(TripSpecificationsHelper.GetTripCriteria(queryParams))
            
        {
            AddInclude(t => t.bus);
            AddInclude(q => q.Include(t => t.Driver).ThenInclude(d => d.User));
            //Include متداخل(ThenInclude)
            AddInclude(q =>
                q.Include(t => t.CityTrips)
                 .ThenInclude(ct => ct.FromCity)
                 .Include(t => t.CityTrips)
                 .ThenInclude(ct => ct.ToCity));


            switch (queryParams.SortingOption)
            {
               case TripSortingOptions.dateOfDepartureAsc:
                    AddOrderBy(t => t.DateOfDeparture);
                    break;
                case TripSortingOptions.dateOfDepartureDesc:
                    AddOrderByDescending(t => t.DateOfDeparture);
                    break;

            }
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        public TripWithCItysSpecification(TripQueryParams queryParams,int BusId) :
            base(TripSpecificationsHelper.GetTripCriteriaWithBusId(queryParams,BusId))

        {
            AddInclude(t => t.bus);

            //Include متداخل(ThenInclude)
            AddInclude(q =>
                q.Include(t => t.CityTrips)
                 .ThenInclude(ct => ct.FromCity)
                 .Include(t => t.CityTrips)
                 .ThenInclude(ct => ct.ToCity));


            switch (queryParams.SortingOption)
            {
                case TripSortingOptions.dateOfDepartureAsc:
                    AddOrderBy(t => t.DateOfDeparture);
                    break;
                case TripSortingOptions.dateOfDepartureDesc:
                    AddOrderByDescending(t => t.DateOfDeparture);
                    break;

            }
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
        public TripWithCItysSpecification (
            string fromCityName,
            string toCityName,
            DateTime dateOfDeparture)
            : base(t =>
                t.DateOfDeparture == dateOfDeparture &&
                t.CityTrips.Any(ct =>
                    (string.IsNullOrEmpty(fromCityName) || ct.FromCity.Name.ToLower().Contains(fromCityName.ToLower())) &&
                    (string.IsNullOrEmpty(toCityName) || ct.ToCity.Name.ToLower().Contains(toCityName.ToLower()))))
        {
            //AddInclude(t => t.bus);
            ////Include متداخل(ThenInclude)
            //AddInclude(q =>
            //    q.Include(t => t.CityTrips)
            //     .ThenInclude(ct => ct.FromCity)
            //     .Include(t => t.CityTrips)
            //     .ThenInclude(ct => ct.ToCity));
        }
    }
}
