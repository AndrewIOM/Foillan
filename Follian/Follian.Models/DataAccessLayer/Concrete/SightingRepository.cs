using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.Models.Occurrence;

namespace Foillan.Models.DataAccessLayer.Concrete
{
    public class SightingRepository : IRepository<Sighting>
    {
        private readonly DbSet<Sighting> _sightings;
        private readonly IFoillanContext _dbContext;

        public Sighting Add(Sighting entity)
        {
            var existing = _sightings.FirstOrDefault(e => e.Id.Equals(entity.Id));
            if (existing != null)
            {
                return null;
            }

            _sightings.Add(entity);
            _dbContext.SaveChanges();
            return _sightings.FirstOrDefault(e => e.Id.Equals(entity.Id));
        }

        public void Update(Sighting entity)
        {
            var existing = _sightings.FirstOrDefault(e => e.Id.Equals(entity.Id));
            if (existing == null)
            {
                return;
            }

            _sightings.Remove(existing);
            _sightings.Add(entity);
        }

        public void Delete(Sighting entity)
        {
            var existing = _sightings.FirstOrDefault(e => e.Id.Equals(entity.Id));
            if (existing == null)
            {
                return;
            }
            _sightings.Remove(existing);
        }

        public Sighting GetById(int id)
        {
            var result = _sightings.FirstOrDefault(t => t.Id == id);
            return result;
        }

        public IEnumerable<Sighting> GetAll()
        {
            return _sightings;
        }

        public IQueryable<Sighting> FindBy(Expression<Func<Sighting, bool>> predicate)
        {
            return _sightings.Where(predicate);
        }
    }
}