using System.Data.Entity;
using Foillan.Models.Biodiversity;

namespace Foillan.Models
{
    public class BiodiversityDbContext : DbContext
    {
        public BiodiversityDbContext()
            : base("FoillanContext")
        {
            Database.SetInitializer(new BiodiversityDataInitialiser());
        }

        public DbSet<Taxon> Taxa { get; set; }
        public DbSet<Sighting> Sightings { get; set; }
        public DbSet<Spotter> Spotter { get; set; }
    }
}
