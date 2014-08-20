using System.Data.Entity;
using Foillan.Models.Account;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccess.Abstract;
using Foillan.Models.Occurrence;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Foillan.Models.DataAccess.Concrete
{
    public class FoillanContext : IdentityDbContext<FoillanUser>, IFoillanContext
    {
        public FoillanContext()
            : base("FoillanContext")
        {
            //Database.SetInitializer();
        }

        public DbSet<Taxon> Taxa { get; set; }
        public DbSet<AdditionalDetails> SpeciesDetails { get; set; }
        public DbSet<Sighting> Sightings { get; set; }

        public static FoillanContext Create()
        {
            return new FoillanContext();
        }
    }
}