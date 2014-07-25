using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models.Biodiversity
{
    //[ParentTaxonValidation(ErrorMessage = "A valid parent taxon is required.")]
    public class Taxon : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required, HiddenInput(DisplayValue = false)]
        public TaxonRank Rank { get; set; }

        [Required]
        public String LatinName { get; set; }

        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        public virtual Taxon ParentTaxon { get; set; }
        public virtual IEnumerable<Taxon> ChildTaxa { get; set; }
    }

    public enum TaxonRank
    {
        Null = 0,
        Life = 1,
        Kingdom = 2,
        Phylum = 3,
        Class = 4,
        Order = 5,
        Family = 6,
        Genus = 7,
        Species = 8,
        Subspecies = 9
    }
}