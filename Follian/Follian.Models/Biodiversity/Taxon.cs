﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Foillan.Models.DataAccess.Abstract;

namespace Foillan.Models.Biodiversity
{
    public class Taxon : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
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