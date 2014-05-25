using System.Data.Entity;
using Follian.Models.Biodiversity;

namespace Follian.Models
{
    public class BiodiversityDbContext : DbContext
    {
        public BiodiversityDbContext()
            : base("FollianContext")
        {
            Database.SetInitializer(new BiodiversityDataInitialiser());
        }

        public DbSet<Taxon> Taxa { get; set; }
        public DbSet<Sighting> Sightings { get; set; }
        public DbSet<Spotter> Spotter { get; set; }
    }
}
