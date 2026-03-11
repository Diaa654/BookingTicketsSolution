using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IDriverRepository<TEntity,TKey> where TEntity : class
    {
        Task<TEntity?> GetByNameAsync(string Name);
    }
}
