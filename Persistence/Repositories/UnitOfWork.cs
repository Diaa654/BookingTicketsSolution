using Domain.Contracts;
using Domain.Modules;
using Domain.Modules.UserModule;
using Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingTicketsDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];
       

        public UnitOfWork(BookingTicketsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDriverRepository<Driver, int> GetDriverRepository()
        {
            var entityType = typeof(Driver);

            if (_repositories.TryGetValue(entityType, out var repository))
                return (IDriverRepository<Driver, int>)repository;

            var newRepository = new DriverRepository(_dbContext);
            _repositories[entityType] = newRepository;

            return newRepository;
        }

       
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType, out var repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository;
            }
            else { 
                var newRepository = new GenericRepository<TEntity, TKey>(_dbContext);
                _repositories[EntityType] = newRepository;
                return newRepository;
            } 
        }

        public async Task<int> SaveChangesAsync() =>await _dbContext.SaveChangesAsync();
          
       

    }
}
    