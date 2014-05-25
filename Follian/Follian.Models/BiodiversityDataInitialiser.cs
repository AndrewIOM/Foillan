using System.Collections.Generic;
using System.Data.Entity;
using Follian.Models.Biodiversity;

namespace Follian.Models
{
    public class BiodiversityDataInitialiser : DropCreateDatabaseAlways<BiodiversityDbContext>
    {
        protected override void Seed(BiodiversityDbContext context)
        {
            IList<Taxon> Taxa = new List<Taxon>();

            var Genus1 = new Taxon
            {
                Rank = TaxonRank.Genus,
                ID = 1,
                LatinName = "Puffinus",
            };

            var Genus2 = new Taxon
            {
                Rank = TaxonRank.Genus,
                ID = 2,
                LatinName = "Fraxinus",
            };

            var Species1 = new Taxon
            {
                Rank = TaxonRank.Species,
                ID = 3,
                LatinName = "puffinus",
                AlternativeNames = new List<AlternativeName> { new AlternativeName { Language = Language.English, Name = "Manx Shearwater" } }
            };

            var Species2 = new Taxon
            {
                Rank = TaxonRank.Species,
                ID = 4,
                LatinName = "Fraxinus",
            };

            Taxa.Add(Genus1);
            Taxa.Add(Genus2);
            Taxa.Add(Species1);
            Taxa.Add(Species2);
            context.Taxa.AddRange(Taxa);
            base.Seed(context);
        }

    }
}
