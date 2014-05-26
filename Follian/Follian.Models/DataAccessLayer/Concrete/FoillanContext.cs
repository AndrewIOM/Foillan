using System.Data.Entity;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models.DataAccessLayer.Concrete
{
    public class FoillanContext : DbContext, IFoillanContext
    {
        public FoillanContext()
            : base("FoillanContext")
        {
            //Database.SetInitializer();
        }

        public DbSet<Taxon> Taxa { get; set; }
        public DbSet<SpeciesDetails> SpeciesDetails { get; set; }
        public DbSet<Sighting> Sightings { get; set; }
        public DbSet<Spotter> Spotter { get; set; }
    }
}