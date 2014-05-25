using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models.DataAccessLayer.Concrete
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected BiodiversityDbContext DbContext;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            DbContext = unitOfWork.DbContext;
        }

        public TEntity Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(object id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
