using Domain.Modules.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.DriverSpecifications
{
    internal class DriverSpecifications: BaseSpecification<Driver, int>
    {
        public DriverSpecifications(int driverId) :
            base(d => d.UserId == driverId)
        {
            AddInclude(d => d.User);
        }
        public DriverSpecifications(string driverName) :
            base(d => (d.User.FirstName + " " + d.User.LastName) == driverName)
        {
            AddInclude(d => d.User);
            AddInclude(d=>d.DriverTrips);
        }
    }
}
