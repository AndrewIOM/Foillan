using System.Data.Entity;
using Foillan.Models.Biodiversity;
using Foillan.Models.Occurrence;

namespace Foillan.Models.DataAccess.Abstract
{
    public interface IFoillanContext
    {
        DbSet<Taxon> Taxa { get; }
        DbSet<AdditionalDetails> SpeciesDetails { get; }

        DbSet<Sighting> Sightings { get; }
        int SaveChanges();
    }
}
