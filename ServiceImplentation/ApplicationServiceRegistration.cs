using Microsoft.Extensions.DependencyInjection;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            Services.AddScoped<IServiceManger, ServiceManger>();
            Services.AddScoped<ITripService, TripService>();
            Services.AddScoped<Func<ITripService>>(provider =>
            () => provider.GetRequiredService<ITripService>());

            Services.AddScoped<IBusService, BusService>();
            Services.AddScoped<Func<IBusService>>(provider =>
            () => provider.GetRequiredService<IBusService>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>());

            


            return Services;
        }
    }
}
