using Domain.Contracts;
using Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Services.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint, 
            ISpecifications<TEntity, TKey> specifications) where TEntity : class
        {
            var query = EntryPoint;
            if (specifications is not null)
            {

                if(specifications.Criteria is not null)
                {
                    query = query.Where(specifications.Criteria);
                }
                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                   query = specifications.IncludeExpressions.Aggregate(query,
                       (current, includeExpression) => current.Include(includeExpression));
                }
                if (specifications.IncludeQueries is not null && specifications.IncludeQueries.Any())
                {
                    foreach (var includeQuery in specifications.IncludeQueries)
                    {
                        query = includeQuery(query);
                    }
                }
                if(specifications.OrderBy is not null)
                {
                    query = query.OrderBy(specifications.OrderBy);
                }
                if(specifications.OrderByDescending is not null)
                {
                    query = query.OrderByDescending(specifications.OrderByDescending);
                }
                if(specifications.IsPaginated)
                {
                    query = query.Skip(specifications.Skip).Take(specifications.Take);
                }
            }
            
            return query;
        }
    }
}
