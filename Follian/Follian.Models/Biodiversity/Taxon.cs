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
        Life = 1,
        Domain = 2,
        Kingdom = 3,
        Phylum = 4,
        Class = 5,
        Order = 6,
        Family = 7,
        Genus = 8,
        Species = 9,
        Subspecies = 10
    }
}