using Domain.Contracts;
using Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal abstract class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> 
    {
        #region Criteria
        public Expression<Func<TEntity, bool>> Criteria { get; }

        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        #endregion

        #region Include
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
           = new List<Expression<Func<TEntity, object>>>();

        // ✅ Advanced includes (Include + ThenInclude)
        public List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> IncludeQueries { get; }
            = new();

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

        protected void AddInclude(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeQuery)
        {
            IncludeQueries.Add(includeQuery);
        }
        #endregion

        #region Sorting 
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

       

        public void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        #endregion

        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }
        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
            IsPaginated = true;
        }
        #endregion

    }
}
