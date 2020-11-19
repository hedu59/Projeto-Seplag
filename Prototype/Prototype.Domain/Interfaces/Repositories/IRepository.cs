using Microsoft.EntityFrameworkCore.Query;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Prototype.Domain.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, bool disableTracking = true);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                           bool disableTracking = false,
                           bool ignoreQueryFilters = false);

        TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                           Expression<Func<TEntity, bool>> predicate = null,
                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                           bool disableTracking = false,
                                           bool ignoreQueryFilters = false);

        IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         int pageIndex = 0,
                                         int pageSize = 10,
                                         bool disableTracking = true,
                                         bool ignoreQueryFilters = false);

        void Save(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
    }
}
