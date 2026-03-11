using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class TripQueryParams
    {
        public string? FromCity { get; set; }
        public string? ToCity { get; set; }
        public DateTime? Date { get; set; }
        public TripSortingOptions SortingOption { get; set; } 
        private int _PageIndex = 1;
        public int PageIndex
        {
            get => _PageIndex;
            set => _PageIndex = (value < 1 )? 1 : value;
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
