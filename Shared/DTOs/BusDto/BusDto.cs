using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.BusDto
{
    public class BusDto
    {
       
        public string PlateNumber { get; set; } = default!;
        public int Capacity { get; set; }
        public string Status { get; set; } = default!;
        public int TripCount { get; set; }
    }
}
