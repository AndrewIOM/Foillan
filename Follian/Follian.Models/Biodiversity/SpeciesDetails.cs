using System.Collections.Generic;

namespace Foillan.Models.Biodiversity
{
    public class SpeciesDetails
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public virtual IEnumerable<AlternativeName> AlternativeNames { get; set; }
        public virtual IEnumerable<Sighting> Sightings { get; set; }
    }
}