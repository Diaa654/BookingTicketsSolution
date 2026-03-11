using Domain.Contracts;
using Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        private readonly BookingTicketsDbContext _dbContext;

        public GenericRepository(BookingTicketsDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);



        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            var query=SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications);
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
           var query= SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications);
           return await query.FirstOrDefaultAsync();
        }

        public Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();
        }
    }
}
