
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.BusDto
{
    public class UpdateBusDto
    {
        public string PlateNumber { get; set; } = default!;
        public int Capacity { get; set; }
        public BusStatus Status { get; set; }
        

    }
}
