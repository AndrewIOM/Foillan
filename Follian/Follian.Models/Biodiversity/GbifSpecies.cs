using System.ComponentModel.DataAnnotations;

namespace Foillan.Models.Biodiversity
{
    public class GbifSpecies
    {
        public int UsageKey { get; set; }
        public string ScientificName { get; set; }
        public string CanonicalName { get; set; }
        public string Rank { get; set; }
        public string MatchType { get; set; }
        public string Kingdom { get; set; }
        public string Phylum { get; set; }
        public string Class { get; set; }
        public string Order { get; set; }
        public string Family { get; set; }
        public string Genus { get; set; }
    }
}
