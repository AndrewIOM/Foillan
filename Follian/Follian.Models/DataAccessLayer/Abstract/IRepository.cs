using System;
using System.Linq;
using System.Linq.Expressions;

namespace Follian.Models.DataAccessLayer.Abstract
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        TEntity GetById(object id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}