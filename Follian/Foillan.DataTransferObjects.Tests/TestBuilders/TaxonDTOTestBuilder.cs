using Foillan.Models.Biodiversity;

namespace Foillan.DataTransferObjects.Tests.TestBuilders
{
    public class TaxonDTOTestBuilder
    {
        private TaxonDTO _taxon;

        public TaxonDTOTestBuilder()
        {
            _taxon = new TaxonDTO();
        }

        public TaxonDTOTestBuilder ValidInitial()
        {
            _taxon = new TaxonDTO
                     {
                         Description = "test dto",
                         Id = 4,
                         LatinName = "Testfish",
                         Rank = TaxonRank.Species,
                         Taxonomy = new Taxonomy
                                    {
                                        Class = "testclass",
                                        Family = "testfamily",
                                        Genus = "testgenus",
                                        Kingdom = "testkingdom",
                                        Order = "testorder",
                                        Phylum = "testphylum"
                                    }
                     };

            return this;
        }

        public TaxonDTO Build()
        {
            return _taxon;
        }
    }
}
