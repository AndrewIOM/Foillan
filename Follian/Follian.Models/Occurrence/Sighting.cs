using Foillan.Models.Account;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccess.Abstract;
using Foillan.Models.Geography;

namespace Foillan.Models.Occurrence
{
    public class Sighting : IEntity<int>
    {
        public int Id { get; set; }
        public int TaxonId { get; set; }
        public int SpotterId { get; set; }
        public virtual Location Location { get; set; }
        public virtual Taxon Taxon { get; set; }
        public virtual FoillanUser SpottedBy { get; set; }
    }
}