using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccess.Concrete;

namespace Foillan.Models.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FoillanContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Foillan.Models.DataAccessLayer.Concrete.FoillanContext";
        }

        protected override void Seed(FoillanContext context)
        {
            var life = new Taxon {Id = 1, Rank = TaxonRank.Life, LatinName = "Life"};
            var kingdom = new Taxon {Id = 2, Rank = TaxonRank.Kingdom, LatinName = "Animalia", ParentTaxon = life};
            var phylum = new Taxon {Id = 3, Rank = TaxonRank.Phylum, LatinName = "Chordata", ParentTaxon = kingdom};
            var @class = new Taxon {Id = 4, Rank = TaxonRank.Class, LatinName = "Aves", ParentTaxon = phylum};
            var order = new Taxon {Id = 5, Rank = TaxonRank.Order, LatinName = "Procellariiformes", ParentTaxon = @class};
            var family = new Taxon {Id = 6, Rank = TaxonRank.Family, LatinName = "Procellariidae", ParentTaxon = order};
            var genus = new Taxon {Id = 7, Rank = TaxonRank.Genus, LatinName = "Puffinus", ParentTaxon = family};
            var species = new Taxon {Id = 8, Rank = TaxonRank.Species, LatinName = "puffinus", ParentTaxon = genus};
            context.Taxa.AddOrUpdate(x => x.Id, life, kingdom, phylum, @class, order, family, genus, species);
        }
    }
}
