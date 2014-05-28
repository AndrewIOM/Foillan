using Foillan.Models.Biodiversity;
using Foillan.Models.ValidationAttributes;
using NUnit.Framework;

namespace Foillan.Models.Tests.ValidationAttributes
{
    [TestFixture]
    public class ParentTaxonValidationAttributeTests
    {
        [Test]
        public void IsValid_NullObjectParameter_Invalid()
        {
            var sut = new ParentTaxonValidationAttribute();
            var result = sut.IsValid(null);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_ModelIsNotTaxonClass_Invalid()
        {
            var model = new Spotter();
            var sut = new ParentTaxonValidationAttribute();
            var result = sut.IsValid(model);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValid_ModelHasNullRank_Invalid()
        {
            var model = new Taxon{Rank = TaxonRank.Null};
            var sut = new ParentTaxonValidationAttribute();
            var result = sut.IsValid(model);
            Assert.IsFalse(result);
        }

        [Test]
        //NB: Parent is not a required field (yet)
        public void IsValid_ParentRankIsNull_Valid()
        {
            var model = new Taxon { Rank = TaxonRank.Null, ParentTaxon = new Taxon{Rank = TaxonRank.Null}};
            var sut = new ParentTaxonValidationAttribute();
            var result = sut.IsValid(model);
            Assert.IsTrue(result);
        }


        [Test]
        public void IsValid_ModelIsLifeRank_NoParentTaxonAllowed()
        {
            var model = new Taxon { Rank = TaxonRank.Life, ParentTaxon = new Taxon { Rank = TaxonRank.Domain } };
            var sut = new ParentTaxonValidationAttribute();
            var result = sut.IsValid(model);
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(TaxonRank.Subspecies, TaxonRank.Species, true)]
        [TestCase(TaxonRank.Subspecies, TaxonRank.Phylum, false)]
        [TestCase(TaxonRank.Species, TaxonRank.Subspecies, false)]
        [TestCase(TaxonRank.Life, TaxonRank.Kingdom, false)]
        [TestCase(TaxonRank.Species, TaxonRank.Genus, true)]
        public void Taxon_RankIsBelowLife_ParentTaxonMustBeOneRankHigher(TaxonRank rank, TaxonRank parentRank,
            bool shouldPassValidation)
        {
            var model = new Taxon { Rank = rank, ParentTaxon = new Taxon { Rank = parentRank } };
            var sut = new ParentTaxonValidationAttribute();
            var result = sut.IsValid(model);
            Assert.AreEqual(shouldPassValidation, result);
        }
    }
}
