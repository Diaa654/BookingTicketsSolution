using Domain.Modules;
using Domain.Modules.BookingsModule;
using Domain.Modules.BusModule;
using Domain.Modules.CityModule;
using Domain.Modules.PaymentModule;
using Domain.Modules.TicketModule;
using Domain.Modules.TripModule;
using Domain.Modules.UserModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.DbContexts
{
    public class BookingTicketsDbContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
        public BookingTicketsDbContext(DbContextOptions<BookingTicketsDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
        
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Trip>Trips { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<CityTrips> CityTrips { get; set; }
    }
}