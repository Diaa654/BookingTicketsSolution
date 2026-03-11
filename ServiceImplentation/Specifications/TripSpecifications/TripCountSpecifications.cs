using Domain.Modules.TripModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.TripSpecifications
{
    internal class TripCountSpecifications : BaseSpecification<Trip, int>
    {
        public TripCountSpecifications(TripQueryParams queryParams) :
            base(TripSpecificationsHelper.GetTripCriteria(queryParams))
        {

        }
        public TripCountSpecifications(TripQueryParams queryParams,int BusID) :
            base(TripSpecificationsHelper.GetTripCriteriaWithBusId(queryParams,BusID))
        {

        }
    }
}
