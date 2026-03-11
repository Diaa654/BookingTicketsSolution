using Domain.Modules.BusModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.BusSpecifications
{
    internal class BusCountSpecifications:BaseSpecification<Bus,int>
    {
            public BusCountSpecifications(BusQueryParams queryParams) :
                base(b =>
              (string.IsNullOrEmpty(queryParams.PlateNumber) || b.PlateNumber.Contains(queryParams.PlateNumber)) &&
              (!queryParams.Status.HasValue || b.Status == queryParams.Status.Value))
            {

            }
    }
}
