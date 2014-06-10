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

            kingdom.ParentTaxon = _taxonRepository.GetById(1);
            var returnedKingdom = AddOrUpdateTaxon(kingdom);

            phylum.ParentTaxon = returnedKingdom;
            var returnedPhylum = AddOrUpdateTaxon(phylum);

            @class.ParentTaxon = returnedPhylum;
            var returnedClass = AddOrUpdateTaxon(@class);

            order.ParentTaxon =returnedClass;
            var returnedOrder = AddOrUpdateTaxon(order);

            family.ParentTaxon = returnedOrder;
            var returnedFamily = AddOrUpdateTaxon(family);

            genus.ParentTaxon = returnedFamily;
            var returnedGenus = AddOrUpdateTaxon(genus);

            species.ParentTaxon = returnedGenus;
            AddOrUpdateTaxon(species);
        }

        private Taxon AddOrUpdateTaxon(Taxon taxon)
        {
            var existingTaxon = _taxonRepository.FindBy(t => t.GbifTaxonId == taxon.GbifTaxonId).FirstOrDefault();
            if (existingTaxon == null)
            {
                var result = _taxonRepository.Add(taxon);
                return result;
            }
            return existingTaxon;
        }

        public virtual void SaveChanges()
        {
            _unitOfWork.Save();
        }
    }
}