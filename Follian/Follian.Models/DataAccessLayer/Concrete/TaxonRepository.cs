using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models.DataAccessLayer.Concrete
{
    public class TaxonRepository : IRepository<Taxon>
    {
        private readonly DbSet<Taxon> _taxa;
        private readonly IFoillanContext _dbContext;

        public TaxonRepository(IUnitOfWork unitOfWork)
        {
            _taxa = unitOfWork.DbContext.Taxa;
            _dbContext = unitOfWork.DbContext;
        }

        public Taxon GetById(int id)
        {
            var result = _taxa.FirstOrDefault(t => t.Id == id);
            return result;
        }

        public IEnumerable<Taxon> GetAll()
        {
            return _taxa;
        }

        public IQueryable<Taxon> FindBy(Expression<Func<Taxon, bool>> predicate)
        {
            return _taxa.Where(predicate);
        }

        public Taxon Add(Taxon entity)
        {
            var existing = _taxa.FirstOrDefault(e => e.Id.Equals(entity.Id));
            if (existing != null)
            {
                return null;
            }

            _taxa.Add(entity);

            try
            {
                _dbContext.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            return _taxa.FirstOrDefault(e => e.Id.Equals(entity.Id));
        }

        public void Update(Taxon entity)
        {
            var existing = _taxa.FirstOrDefault(e => e.Id.Equals(entity.Id));
            if (existing == null)
            {
                return;
            }

            _taxa.Remove(existing);
            _taxa.Add(entity);
        }

        public void Delete(Taxon entity)
        {
            var existing = _taxa.FirstOrDefault(e => e.Id.Equals(entity.Id));
            if (existing == null)
            {
                return;
            }
            _taxa.Remove(existing);
        }
    }
}
