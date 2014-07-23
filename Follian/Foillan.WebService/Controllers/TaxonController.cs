using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Foillan.DataTransferObjects;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.WebService.Controllers
{
    [RoutePrefix("api/Taxon")]
    public class TaxonController : ApiController
    {
        private readonly ITaxonService _taxonService;

        public TaxonController(ITaxonService taxonServiceIn)
        {
            _taxonService = taxonServiceIn;
        }

        //GET: /Api/Taxon?rank={rank}
        public IEnumerable<TaxonDTO> Get(TaxonRank rank)
        {
            var taxaOfRank = from t in _taxonService.GetTaxaByRank(rank)
                             select new TaxonDTO
                                    {
                                        Id = t.Id,
                                        Rank = t.Rank,
                                        LatinName = t.LatinName,
                                        Description = t.Description ?? String.Empty,
                                        Taxonomy = new Taxonomy
                                        {
                                            Kingdom = GetLatinRankForTaxon(TaxonRank.Kingdom, t),
                                            Order = GetLatinRankForTaxon(TaxonRank.Order, t),
                                            Phylum = GetLatinRankForTaxon(TaxonRank.Phylum, t),
                                            Class = GetLatinRankForTaxon(TaxonRank.Class, t),
                                            Genus = GetLatinRankForTaxon(TaxonRank.Genus, t),
                                            Species = GetLatinRankForTaxon(TaxonRank.Species, t),
                                            SubSpecies = GetLatinRankForTaxon(TaxonRank.Subspecies, t),
                                        }
                                    };
            return taxaOfRank;
        }

        //GET: /Api/Taxon/{id}
        public TaxonDTO GetTaxon(int id)
        {
            var taxon = _taxonService.GetTaxonById(id);

            var dto = new TaxonDTO()
                      {
                          Id = taxon.Id,
                          Rank = taxon.Rank,
                          LatinName = taxon.LatinName,
                          Description = taxon.Description ?? String.Empty,
                          Taxonomy = new Taxonomy
                                     {
                                         Kingdom = GetLatinRankForTaxon(TaxonRank.Kingdom, taxon),
                                         Order = GetLatinRankForTaxon(TaxonRank.Order, taxon),
                                         Family = GetLatinRankForTaxon(TaxonRank.Family, taxon),
                                         Phylum = GetLatinRankForTaxon(TaxonRank.Phylum, taxon),
                                         Class = GetLatinRankForTaxon(TaxonRank.Class, taxon),
                                         Genus = GetLatinRankForTaxon(TaxonRank.Genus, taxon),
                                         Species = GetLatinRankForTaxon(TaxonRank.Species, taxon),
                                         SubSpecies = GetLatinRankForTaxon(TaxonRank.Subspecies, taxon),
                                     }

                      };

            return dto;
        }

        // POST /api/Taxon
        public void Post(TaxonDTO newTaxonDto)
        {
            _taxonService.AddSpeciesWithHeirarchy(new Taxon
                                                  {
                                                      Description = newTaxonDto.Description,
                                                      LatinName = newTaxonDto.LatinName,
                                                      Rank = TaxonRank.Species
                                                  }, new Dictionary<TaxonRank, string> {
                                                  {TaxonRank.Class, newTaxonDto.Taxonomy.Class},
                                                  {TaxonRank.Family, newTaxonDto.Taxonomy.Family},
                                                  {TaxonRank.Genus, newTaxonDto.Taxonomy.Genus},
                                                  {TaxonRank.Kingdom, newTaxonDto.Taxonomy.Kingdom},
                                                  {TaxonRank.Order, newTaxonDto.Taxonomy.Order},
                                                  {TaxonRank.Phylum, newTaxonDto.Taxonomy.Phylum},
                                                  {TaxonRank.Species, newTaxonDto.Taxonomy.Species},
                                                  {TaxonRank.Subspecies, newTaxonDto.Taxonomy.SubSpecies},
                                                  });
            _taxonService.SaveChanges();
        }

        //PUT /api/Taxon/{id}
        public void PUT(int id, TaxonDTO updatedDto)
        {
            var existingTaxon = _taxonService.GetTaxonById(updatedDto.Id);
            existingTaxon.Description = updatedDto.Description;
            existingTaxon.LatinName = updatedDto.LatinName;
            _taxonService.SaveChanges();
        }

        private static string GetLatinRankForTaxon(TaxonRank rank, Taxon taxon)
        {
            var Ranks = new List<Taxon>();
            var CurrentRank = taxon;
            while (CurrentRank != null)
            {
                Ranks.Add(CurrentRank);
                CurrentRank = CurrentRank.ParentTaxon;
            }

            var AncestorOfRank = Ranks.FirstOrDefault(p => p.Rank.Equals(rank));
            return AncestorOfRank != null ? AncestorOfRank.LatinName : String.Empty;
        }
    }
}