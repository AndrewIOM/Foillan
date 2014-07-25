using System.Data.Entity;
using Foillan.Models.Biodiversity;
using Foillan.Models.Occurrence;

namespace Foillan.Models.DataAccessLayer.Abstract
{
    public interface IFoillanContext
    {
        DbSet<Taxon> Taxa { get; }
        DbSet<AdditionalDetails> SpeciesDetails { get; }

        DbSet<Sighting> Sightings { get; }
        DbSet<Spotter> Spotter { get; }
        int SaveChanges();
    }
}
