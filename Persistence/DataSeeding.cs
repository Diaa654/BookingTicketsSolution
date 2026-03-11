using Domain.Contracts;
using Domain.Modules.UserModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(BookingTicketsDbContext _dbContext
        , UserManager<User> _userManager,
        RoleManager<IdentityRole<int>> _roleManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole<int>("Passenger"));
                    await _roleManager.CreateAsync(new IdentityRole<int>("Driver"));
                }
                if (!_userManager.Users.Any())
                {
                    var user01 = new User { Email = "diaaemad156@gmail.com", FirstName = "Diaa", LastName = "Emad", PhoneNumber = "01232556656", UserName = "DiaaEmad" };
                    var user02 = new User { Email = "Mohamed@gmail.com", FirstName = "Mohamed", LastName = "Emad", PhoneNumber = "01232556656", UserName = "MohamedEmad" };
                    var user03 = new User { Email = "Ali@gmail.com", FirstName = "Ali", LastName = "Ahmed", PhoneNumber = "01232556656", UserName = "AliAhmed" };

                   
                    await _userManager.CreateAsync(user01, "P@ssw0rd");
                    await _userManager.CreateAsync(user02, "P@ssw0rd");
                    await _userManager.CreateAsync(user03, "P@ssw0rd");

                   
                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "Passenger");
                    await _userManager.AddToRoleAsync(user03, "Driver");

                    
                    var passenger = new Passenger
                    {
                        UserId = user02.Id, 
                        DateOfBirth = new DateTime(1995, 1, 1),
                        CreatedAt = DateTime.Now
                    };

                    var driver = new Driver
                    {
                        UserId = user03.Id,
                        LicenseNumber = "ABC-123",
                        ExperienceYears = 5,
                        IsAvailable = true,
                        JoinedAt = DateTime.Now,
                        Address= "123 Main St",
                        PictureUrl="//123"
                    };

                    await _dbContext.Set<Passenger>().AddAsync(passenger);
                    await _dbContext.Set<Driver>().AddAsync(driver);

                    
                    await _dbContext.SaveChangesAsync();
                }


               
            }
            catch (Exception ex)
            {
                //to do
            }


        }
        
    }
}
