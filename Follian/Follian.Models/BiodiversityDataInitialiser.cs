using System.Collections.Generic;
using System.Data.Entity;
using Foillan.Models.Biodiversity;

namespace Foillan.Models
{
    public class BiodiversityDataInitialiser : DropCreateDatabaseAlways<BiodiversityDbContext>
    {
        protected override void Seed(BiodiversityDbContext context)
        {
            IList<Taxon> taxa = new List<Taxon>();

            var genus1 = new Taxon
            {
                Rank = TaxonRank.Genus,
                ID = 1,
                LatinName = "Puffinus",
            };

            var genus2 = new Taxon
            {
                Rank = TaxonRank.Genus,
                ID = 2,
                LatinName = "Fraxinus",
            };

            var species1 = new Taxon
            {
                Rank = TaxonRank.Species,
                ID = 3,
                LatinName = "puffinus",
            };

            var species2 = new Taxon
            {
                Rank = TaxonRank.Species,
                ID = 4,
                LatinName = "Fraxinus",
            };

            taxa.Add(genus1);
            taxa.Add(genus2);
            taxa.Add(species1);
            taxa.Add(species2);
            context.Taxa.AddRange(taxa);
            base.Seed(context);
        }

    }
}
