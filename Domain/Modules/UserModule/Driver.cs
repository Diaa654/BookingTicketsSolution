using Domain.Modules.TripModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.UserModule
{
    public class Driver
    {
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public string LicenseNumber { get; set; } = default!;
        public DateTime LicenseExpiryDate { get; set; }
        public int ExperienceYears { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime JoinedAt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PictureUrl { get; set; } = default!;
        public string Address { get; set; } = default!;
        public ICollection<Trip> DriverTrips { get; set; } = [];

    }
}
