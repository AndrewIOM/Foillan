using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.Models.ValidationAttributes;

namespace Foillan.Models.Biodiversity
{
    [ParentTaxonValidation]
    public class Taxon : IEntity<int>
    {
        [Key, Required, HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public TaxonRank Rank { get; set; }

        [Required, Display(Name = "Latin Name")]
        public String LatinName { get; set; }
        public String Description { get; set; }

        public virtual Taxon ParentTaxon { get; set; }
        public virtual IEnumerable<Taxon> ChildTaxa { get; set; }
    }

    public enum TaxonRank
    {
        Null = 0,
        Subspecies = 1,
        Species = 2,
        Genus = 3,
        Family = 4,
        Order = 5,
        Class = 6,
        Phylum = 7,
        Kingdom = 8,
        Domain = 9,
        Life = 10
    }
}