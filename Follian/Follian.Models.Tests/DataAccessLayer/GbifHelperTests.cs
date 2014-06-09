using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer;
using NUnit.Framework;

namespace Foillan.Models.Tests.DataAccessLayer
{
    [TestFixture]
    public class GbifHelperTests
    {
        [Test]
        public void GenerateTaxonByNameAndRank_NoMatchInGbif_ReturnsNull()
        {
            var result = GbifHelpers.GenerateTaxonByNameAndRank("Fake Species", TaxonRank.Species);
            Assert.IsNull(result);
        }

        [Test]
        public void GenerateTaxonByNameAndRank_MatchesGbifRecord_ReturnsTaxon()
        {
            var result = GbifHelpers.GenerateTaxonByNameAndRank("Procellariiformes", TaxonRank.Order);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GenerateTaxonByNameAndRank_MatchesGbifRecord_ReturnedTaxonHasRank()
        {
            var result = GbifHelpers.GenerateTaxonByNameAndRank("Procellariiformes", TaxonRank.Order);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(TaxonRank.Null, result.Rank, "The rank was returned as null");
        }

        [Test]
        public void GenerateTaxonByNameAndRank_MatchesGbifRecord_ReturnedTaxonHasGbifId()
        {
            var result = GbifHelpers.GenerateTaxonByNameAndRank("Procellariiformes", TaxonRank.Order);
            Assert.IsNotNull(result);
            Assert.Greater(result.GbifTaxonId, 0);
        }

        [Test]
        public void GetTaxonById_MatchesGbifRecord_ReturnsPopulatedTaxon()
        {
            var result = GbifHelpers.GetTaxonById(4408612);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.LatinName);
            Assert.Greater(result.GbifTaxonId, 0);
            Assert.AreNotEqual(TaxonRank.Null, result.Rank, "The rank was returned as null");
        }

        [Test]
        public void GetTaxonById_InvalidGbifId_ReturnsNull()
        {
            var result = GbifHelpers.GetTaxonById(12345678);
            Assert.IsNull(result);
        }

        [Test]
        public void GetTaxonomyDictionary_InvalidGbifId_ReturnsNull()
        {
            var result = GbifHelpers.GetTaxonomyDictionary(12345678);
            Assert.IsNull(result);
        }

        [Test]
        public void GetTaxonomyDictionary_ValidGbifId_ReturnsDictionaryWithEachRank()
        {
            var result = GbifHelpers.GetTaxonomyDictionary(4408612);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey(TaxonRank.Kingdom));
            Assert.IsTrue(result.ContainsKey(TaxonRank.Phylum));
            Assert.IsTrue(result.ContainsKey(TaxonRank.Class));
            Assert.IsTrue(result.ContainsKey(TaxonRank.Order));
            Assert.IsTrue(result.ContainsKey(TaxonRank.Family));
            Assert.IsTrue(result.ContainsKey(TaxonRank.Genus));
        }
    }
}
