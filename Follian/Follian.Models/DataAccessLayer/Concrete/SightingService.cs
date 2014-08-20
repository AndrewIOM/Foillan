using System;
using System.Linq;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.Models.Occurrence;

namespace Foillan.Models.DataAccessLayer.Concrete
{
    public class SightingService : ISightingService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Sighting> _repository;

        public SightingService(IUnitOfWork unitOfWork, IRepository<Sighting> taxonRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = taxonRepository;
        }

        public Sighting AddSighting(Sighting newSighting)
        {
            if (newSighting == null)
            {
                throw new ArgumentException("Sighting cannot be null");
            }

            return _repository.Add(newSighting);
        }

        public IQueryable<Sighting> GetAll()
        {
            var sightings = _repository.GetAll().AsQueryable();
            return sightings;
        }

        public Sighting GetSighting(int id)
        {
            var existing = _repository.GetById(id);
            return existing;
        }

        public void SaveChanges()
        {
            _unitOfWork.Save();
        }
    }
}