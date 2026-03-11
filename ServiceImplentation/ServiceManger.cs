using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManger(Func<ITripService> TripFactory,
        Func<IBusService> BusFactory,
        Func<IAuthenticationService> AuthenticationFactory
        ) : IServiceManger
    {
        
        public IAuthenticationService authenticationService => AuthenticationFactory.Invoke();

      
        public ITripService tripService => TripFactory.Invoke();

        public IBusService busService => BusFactory.Invoke();
    }
}
