using System.ComponentModel.DataAnnotations;

namespace Foillan.DataTransferObjects
{
    public class Taxonomy
    {
        [Required]
        public string Kingdom { get; set; }
        [Required]
        public string Phylum { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public string Order { get; set; }
        [Required]
        public string Family { get; set; }
        [Required]
        public string Genus { get; set; }
        [Required]
        public string Species { get; set; }
        [Required]
        public string SubSpecies { get; set; }
    }
}