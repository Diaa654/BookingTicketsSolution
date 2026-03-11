using Domain.Contracts;
using Domain.Modules.UserModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class DriverRepository : IDriverRepository<Driver, int>
    {
        private readonly BookingTicketsDbContext _dbContext;

        public DriverRepository(BookingTicketsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Driver?> GetByNameAsync(string name)
        {
            return await _dbContext.Drivers
                .Include(d => d.User)
                .FirstOrDefaultAsync(d =>
                    d.User != null &&
                    d.User.UserName != null &&
                    d.User.UserName.ToLower().Contains(name.ToLower())
                );
        }
    }
}
