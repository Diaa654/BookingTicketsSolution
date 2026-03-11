using Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity,TKey> 
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        public List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> IncludeQueries { get; }
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginated { get; }

    }
}
