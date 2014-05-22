using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follian.Models
{
    public enum TaxonRank
    {
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

    public class Taxon
    {
        public int ID { get; set; }

        public TaxonRank Rank { get; set; }
        public String LatinName { get; set; }
        public String Description { get; set; }
        public byte[] Image { get; set; }

        public virtual IEnumerable<AlternativeName> AlternativeNames { get; set; }
        public virtual IEnumerable<Sighting> Sightings { get; set; }
    }
}