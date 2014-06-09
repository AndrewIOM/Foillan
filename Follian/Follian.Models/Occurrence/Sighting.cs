using System.ComponentModel.DataAnnotations;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.Models.Geography;

namespace Foillan.Models.Occurrence
{
    public class Sighting : IEntity<int>
    {
        public int Id { get; set; }
        [UIHint("TaxonPicker")]
        public int TaxonId { get; set; }
        public int SpotterId { get; set; }

        [UIHint("GoogleMapPicker")]
        public virtual Location Location { get; set; }
        public virtual Taxon Taxon { get; set; }
        public virtual Spotter SpottedBy { get; set; }
    }
}