using System.Data.Entity;
using Foillan.Models.Biodiversity;

namespace Foillan.Models.DataAccessLayer.Abstract
{
    public interface IFoillanContext
    {
        DbSet<Taxon> Taxa { get; }
        DbSet<Sighting> Sightings { get; }
        DbSet<Spotter> Spotter { get; }
        int SaveChanges();
    }
}
