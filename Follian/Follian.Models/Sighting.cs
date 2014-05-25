using Follian.Models.Biodiversity;
using Follian.Models.DataAccessLayer.Concrete;

namespace Follian.Models
{
    public class Sighting : Entity<int>
    {
        public int ID { get; set; }
        public int TaxonID { get; set; }
        public int SpotterID { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public virtual Taxon Taxon { get; set; }
        public virtual Spotter SpottedBy { get; set; }
    }
}