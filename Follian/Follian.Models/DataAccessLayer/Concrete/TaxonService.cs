using System;
using System.Collections.Generic;
using System.Linq;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models.DataAccessLayer.Concrete
{
    public class TaxonService : ITaxonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Taxon> _taxonRepository;

        public TaxonService(IUnitOfWork unitOfWork, IRepository<Taxon> taxonRepository)
        {
            _unitOfWork = unitOfWork;
            _taxonRepository = taxonRepository;
        }

        public virtual IEnumerable<Taxon> GetTaxaByRank(TaxonRank rank)
        {
            var taxaOfRank = _taxonRepository.GetAll().Where(t => t.Rank.Equals(rank));
            return taxaOfRank;
        }

        public virtual void AddTaxon(Taxon taxon)
        {
            _taxonRepository.Add(taxon);
        }
        
        public void AddSpeciesWithHeirarchy(Taxon species, IDictionary<TaxonRank, String> heirarchyDictionary)
        {
            Taxon kingdom;
            Taxon phylum;
            Taxon @class;
            Taxon order;
            Taxon family;
            Taxon genus;

            try
            {
                kingdom = GbifHelpers.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Kingdom], TaxonRank.Kingdom);
                phylum = GbifHelpers.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Phylum], TaxonRank.Phylum);
                @class = GbifHelpers.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Class], TaxonRank.Class);
                order = GbifHelpers.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Order], TaxonRank.Order);
                family = GbifHelpers.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Family], TaxonRank.Family);
                genus = GbifHelpers.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Genus], TaxonRank.Genus);
            }
            catch (KeyNotFoundException)
            {
                return;
            }

            kingdom.ParentTaxon = null;
            phylum.ParentTaxon = kingdom;
            @class.ParentTaxon = phylum;
            order.ParentTaxon = @class;
            family.ParentTaxon = order;
            genus.ParentTaxon = family;
            species.ParentTaxon = genus;

            _taxonRepository.Add(kingdom);
            _taxonRepository.Add(phylum);
            _taxonRepository.Add(@class);
            _taxonRepository.Add(order);
            _taxonRepository.Add(family);
            _taxonRepository.Add(genus);
            _taxonRepository.Add(species);
        }

        public virtual void SaveChanges()
        {
            _unitOfWork.Save();
        }
    }
}