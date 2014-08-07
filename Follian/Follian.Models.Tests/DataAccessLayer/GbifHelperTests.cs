using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer;
using NUnit.Framework;
using System;

namespace Foillan.Models.Tests.DataAccessLayer
{
    [TestFixture]
    public class GbifHelperTests
    {
        [Test]
        public void GenerateTaxonByNameAndRank_NoMatchInGbif_ReturnsNull()
        {
            Assert.Throws<Exception>(() => GbifHelper.GenerateTaxonByNameAndRank("Fake Species", TaxonRank.Species));
        }

        [Test]
        public void GenerateTaxonByNameAndRank_MatchesGbifRecord_ReturnsTaxon()
        {
            var result = GbifHelper.GenerateTaxonByNameAndRank("Procellariiformes", TaxonRank.Order);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GenerateTaxonByNameAndRank_MatchesGbifRecord_ReturnedTaxonHasRank()
        {
            var result = GbifHelper.GenerateTaxonByNameAndRank("Procellariiformes", TaxonRank.Order);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(TaxonRank.Null, result.Rank, "The rank was returned as null");
        }

        [Test]
        public void GetTaxonById_MatchesGbifRecord_ReturnsPopulatedTaxon()
        {
            var result = GbifHelper.GetTaxonById(4408612);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.LatinName);
            Assert.AreNotEqual(TaxonRank.Null, result.Rank, "The rank was returned as null");
        }

        [Test]
        public void GetTaxonById_InvalidGbifId_ReturnsNull()
        {
            var result = GbifHelper.GetTaxonById(12345678);
            Assert.IsNull(result);
        }

        [Test]
        public void GetTaxonomyDictionary_InvalidGbifId_ReturnsNull()
        {
            var result = GbifHelper.GetTaxonomyDictionary(12345678);
            Assert.IsNull(result);
        }

        [Test]
        public void GetTaxonomyDictionary_ValidGbifId_ReturnsDictionaryWithEachRank()
        {
            var result = GbifHelper.GetTaxonomyDictionary(4408612);
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
