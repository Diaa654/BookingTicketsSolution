using Domain.Modules.TripModule;
using Shared.DTOs.BusDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.BusModule
{
    public class Bus
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; } = default!;
        public int Capacity { get; set; }
        public BusStatus Status { get; set; } = default!;
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
