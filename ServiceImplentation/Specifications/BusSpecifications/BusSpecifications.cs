using Domain.Modules.BusModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Services.Specifications.BusSpecifications
{
    internal class BusSpecifications : BaseSpecification<Bus, int>
    {
        public BusSpecifications(int id) : base(b => b.Id == id)
        {
            AddInclude(b => b.Trips);

            

        }
        public BusSpecifications(string PlateNumber) : base(b => b.PlateNumber == PlateNumber)
        {
            AddInclude(b => b.Trips);
        }
        public BusSpecifications(BusQueryParams busSearch) : base(b =>
           (string.IsNullOrEmpty(busSearch.PlateNumber) || b.PlateNumber.Contains(busSearch.PlateNumber)) &&
           (!busSearch.Status.HasValue || b.Status == busSearch.Status.Value))
        {
            AddInclude(b => b.Trips);
            switch (busSearch.BusSortingOptions)
            {
                case BusSortingOptions.plateNumberAsc:
                    AddOrderBy(b => b.PlateNumber);
                    break;
                case BusSortingOptions.plateNumberDesc:
                    AddOrderByDescending(b => b.PlateNumber);
                    break;
                case BusSortingOptions.CapacityAsc:
                    AddOrderBy(b => b.Capacity);
                    break;
                case BusSortingOptions.CapacityDesc:
                    AddOrderByDescending(b => b.Capacity);
                    break;
                default:
                    AddOrderBy(b => b.Id);
                    break;
            }
            ApplyPagination(busSearch.PageSize, busSearch.PageIndex);
        }

    }
}
