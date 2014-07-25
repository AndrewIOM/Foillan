using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Foillan.Models.Biodiversity;
using NUnit.Framework;

namespace Foillan.Models.Tests.Biodiversity
{
    [TestFixture]
    public class TaxonValidationTests
    {
        [Test]
        [TestCase(TaxonRank.Family, true)]
        [TestCase(null, false)]
        public void Taxon_TaxonRank_RequiredValue(TaxonRank value, bool expected)
        {
            var taxon = new Taxon { Rank = value };
            var validationResult = ValidateModel(taxon);
            var result = !ValidationContainsKey(validationResult, "Rank");
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("puffinus", true)]
        [TestCase(null, false)]
        public void Taxon_LatinName_RequiredValue(String value, bool expectedPass)
        {
            var taxon = new Taxon { LatinName = value };
            var validationResult = ValidateModel(taxon);
            var result = !ValidationContainsKey(validationResult, "LatinName");
            Assert.AreEqual(expectedPass, result);
        }

        [Test]
        [TestCase("Awesome bird", true)]
        [TestCase(null, true)]
        public void Taxon_Description_Optional(String value, bool expectedPass)
        {
            var taxon = new Taxon { Description = value };
            var validationResult = ValidateModel(taxon);
            var result = !ValidationContainsKey(validationResult, "Description");
            Assert.AreEqual(expectedPass, result);
        }

        [Test]
        public void Taxon_IsLifeRank_CannotHaveParentTaxon()
        {
            var parentTaxon = new Taxon {Rank = TaxonRank.Kingdom};
            var taxon = new Taxon { Rank = TaxonRank.Life, ParentTaxon = parentTaxon};
            var validationResult = ValidateModel(taxon);
            var result = ValidationContainsKey(validationResult, "ParentTaxon");
            Assert.IsTrue(result);
        }

        [Test]
        public void Taxon_RankIsNull_ParentTaxonValidationError()
        {
            var parentTaxon = new Taxon { Rank = TaxonRank.Kingdom };
            var taxon = new Taxon { Rank = TaxonRank.Null, ParentTaxon = parentTaxon };
            var validationResult = ValidateModel(taxon);
            var result = ValidationContainsKey(validationResult, "ParentTaxon");
            Assert.IsTrue(result);
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
            var parentTaxon = new Taxon { Rank = parentRank };
            var taxon = new Taxon { Rank = rank, ParentTaxon = parentTaxon };
            var validationResult = ValidateModel(taxon);
            var result = !ValidationContainsKey(validationResult, "ParentTaxon");
            Assert.AreEqual(shouldPassValidation, result);
        }

        private static IEnumerable<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        private static bool ValidationContainsKey(IEnumerable<ValidationResult> results, String key )
        {
            var containsKey = false;
            foreach (var result in results)
            {
                foreach (var member in result.MemberNames)
                {
                    if (member.Equals(key))
                    {
                        containsKey = true;
                    }
                }
            }
            return containsKey;
        }
    }
}
