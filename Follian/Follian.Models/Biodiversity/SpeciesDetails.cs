using System.Collections.Generic;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models.Biodiversity
{
    public class SpeciesDetails : IEntity<int>
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public virtual IEnumerable<AlternativeName> AlternativeNames { get; set; }
        public virtual IEnumerable<Sighting> Sightings { get; set; }
    }
}