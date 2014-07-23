using System.ComponentModel.DataAnnotations;
using Foillan.Models.Biodiversity;

namespace Foillan.DataTransferObjects
{
    public class TaxonDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public TaxonRank Rank { get; set; }

        [Required]
        public string LatinName { get; set; }

        public string Description { get; set; }

        [Required]
        public Taxonomy Taxonomy { get; set; }
    }
}