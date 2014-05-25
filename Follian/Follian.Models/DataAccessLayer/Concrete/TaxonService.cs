using System.Collections.Generic;
using Follian.Models.Biodiversity;
using Follian.Models.DataAccessLayer.Abstract;

namespace Follian.Models.DataAccessLayer.Concrete
{
    public class TaxonService : ITaxonService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IRepository<Taxon> _taxonRepository;

        public TaxonService(IUnitOfWork unitOfWork, IRepository<Taxon> taxonRepository)
        {
            _unitOfWork = unitOfWork;
            _taxonRepository = taxonRepository;
        }

        public virtual IEnumerable<Taxon> GetTaxaByRank(TaxonRank rank)
        {
            var taxaOfRank = _taxonRepository.FindBy(t => t.Rank == rank);
            return taxaOfRank;
        }

        public virtual void SaveChanges()
        {
            _unitOfWork.Save();
        }
    }
}