
using Shared.DTOs.BusDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class BusQueryParams
    {
        public string? PlateNumber { get; set; }
        public BusStatus? Status { get; set; }
        public BusSortingOptions BusSortingOptions { get; set; }
        private int _PageIndex = 1;
        public int PageIndex
        {
            get => _PageIndex;
            set => _PageIndex = (value < 1) ? 1 : value;
        }

        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        private int _PageSize = DefaultPageSize;
        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value < 1) ? DefaultPageSize : (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
