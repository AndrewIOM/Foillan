using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Foillan.DataTransferObjects;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccess.Abstract;

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
        public IEnumerable<TaxonDTO> Get(TaxonRank rank, String match = "")
        {
            if (rank.Equals(TaxonRank.Null))
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                throw new HttpResponseException(response);
            }

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

            if (!String.IsNullOrEmpty(match))
            {
                taxaOfRank =
                    taxaOfRank.Where(t => t.Description.Contains(match) || t.LatinName.Contains(match));
            }

            return taxaOfRank.OrderBy(t => t.LatinName);
        }

        //GET: /Api/Taxon?parent={id}
        [ResponseType(typeof(TaxonDTO))]
        public IHttpActionResult Get(int ParentId)
        {
            var parent = _taxonService.GetTaxonById(ParentId);
            if (parent == null)
            {
                return BadRequest("The parent taxa specified does not exist");
            }

            if (parent.ChildTaxa.Any())
            {
                var childTaxa = from t in parent.ChildTaxa
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
                return Ok(childTaxa);
            }
            else
            {
                return BadRequest(String.Format("A taxon with the ID {0} does not exist", ParentId));
            }
        }

        //GET: /Api/Taxon/{id}
        [ResponseType(typeof(TaxonDTO))]
        public IHttpActionResult GetTaxon(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var taxon = _taxonService.GetTaxonById(id);

            if (taxon == null)
            {
                return NotFound();
            }

            var dto = new TaxonDTO
                      {
                          Id = taxon.Id,
                          Rank = taxon.Rank,
                          LatinName = taxon.LatinName,
                          Description = taxon.Description,
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

            return Ok(dto);
        }

        // POST /api/Taxon
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(TaxonDTO newTaxonDto)
        {
            if (newTaxonDto == null)
            {
                return BadRequest("You must post a TaxonDTO");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taxonomy = new Taxonomy
            {
                Kingdom = newTaxonDto.Taxonomy.Kingdom,
                Phylum = newTaxonDto.Taxonomy.Phylum,
                Order = newTaxonDto.Taxonomy.Order,
                Class = newTaxonDto.Taxonomy.Class,
                Family = newTaxonDto.Taxonomy.Family,
                Genus = newTaxonDto.Taxonomy.Genus,
                Species = newTaxonDto.Taxonomy.Species,
                SubSpecies = newTaxonDto.Taxonomy.SubSpecies
            };

            var newTaxon = new Taxon
                           {
                               Description = newTaxonDto.Description,
                               LatinName = newTaxonDto.LatinName,
                               Rank = newTaxonDto.Rank,
                               Id = newTaxonDto.Id
                           };

            _taxonService.AddTaxon(newTaxon, taxonomy);
            _taxonService.SaveChanges();

            return Ok();
        }

        //PUT /api/Taxon/{id}
        [ResponseType(typeof(TaxonDTO))]
        public IHttpActionResult Put(int id, TaxonDTO updatedDto)
        {
            if (updatedDto == null || id == 0)
            {
                return BadRequest();
            }

            var existingTaxon = _taxonService.GetTaxonById(updatedDto.Id);

            if (existingTaxon == null)
            {
                return BadRequest(String.Format("A taxon with the ID {0} does not exist", id));
            }

            existingTaxon.Description = updatedDto.Description;
            existingTaxon.LatinName = updatedDto.LatinName;
            _taxonService.SaveChanges();

            return Ok(updatedDto);
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