using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models
{
    public class Sighting : IEntity<int>
    {
        public int Id { get; set; }
        public int TaxonId { get; set; }
        public int SpotterId { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public virtual Taxon Taxon { get; set; }
        public virtual Spotter SpottedBy { get; set; }
    }
}