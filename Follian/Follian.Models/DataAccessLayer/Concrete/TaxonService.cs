using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public virtual Taxon AddTaxon(Taxon taxon)
        {
            var saved = _taxonRepository.Add(taxon);
            return saved;
        }

        public Taxon GetTaxonById(int id)
        {
            return _taxonRepository.GetById(id);
        }

        public Taxon AddSpeciesWithHeirarchy(Taxon species, IDictionary<TaxonRank, String> heirarchyDictionary)
        {
            if (species.GbifTaxonId == 0)
            {
                throw new ArgumentException("The species cannot have a GBIF ID of 0.");
            }

            Taxon kingdom;
            Taxon phylum;
            Taxon @class;
            Taxon order;
            Taxon family;
            Taxon genus;

            try
            {
                kingdom = GbifHelper.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Kingdom], TaxonRank.Kingdom);
                phylum = GbifHelper.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Phylum], TaxonRank.Phylum);
                @class = GbifHelper.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Class], TaxonRank.Class);
                order = GbifHelper.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Order], TaxonRank.Order);
                family = GbifHelper.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Family], TaxonRank.Family);
                genus = GbifHelper.GenerateTaxonByNameAndRank(heirarchyDictionary[TaxonRank.Genus], TaxonRank.Genus);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }

            kingdom.ParentTaxon = _taxonRepository.GetById(1);
            var returnedKingdom = AddOrUpdateTaxon(kingdom);

            phylum.ParentTaxon = returnedKingdom;
            var returnedPhylum = AddOrUpdateTaxon(phylum);

            @class.ParentTaxon = returnedPhylum;
            var returnedClass = AddOrUpdateTaxon(@class);

            order.ParentTaxon = returnedClass;
            var returnedOrder = AddOrUpdateTaxon(order);

            family.ParentTaxon = returnedOrder;
            var returnedFamily = AddOrUpdateTaxon(family);

            genus.ParentTaxon = returnedFamily;
            var returnedGenus = AddOrUpdateTaxon(genus);

            species.ParentTaxon = returnedGenus;
            var returnedSpecies = AddOrUpdateTaxon(species);

            return returnedSpecies;
        }

        public virtual IEnumerable<Taxon> GetTaxaByRank(TaxonRank rank)
        {
            var taxaOfRank = _taxonRepository.GetAll().Where(t => t.Rank.Equals(rank));
            return taxaOfRank;
        }

        public virtual void SaveChanges()
        {
            _unitOfWork.Save();
        }

        public SpeciesDetails UpdateSpeciesImage(Taxon taxon, HttpPostedFileBase image)
        {
            throw new NotImplementedException();
        }

        public SpeciesDetails UpdateSpeciesAdditionalNames(IEnumerable<AlternativeName> newNames)
        {
            throw new NotImplementedException();
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
    }
}