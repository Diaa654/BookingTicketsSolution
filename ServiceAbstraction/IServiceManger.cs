using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IServiceManger
    {
        public ITripService tripService { get; }
        public IBusService busService { get; }
        public IAuthenticationService authenticationService { get; }
         
    }
}
