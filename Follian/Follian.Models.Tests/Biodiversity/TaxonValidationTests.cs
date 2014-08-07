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
        public void TaxonRankIsRequired()
        {
            var taxon = new Taxon();
            var sut = ValidateModel(taxon);
            Assert.IsTrue(!ValidationContainsKey(sut, "Rank"));
        }

        [Test]
        [TestCase("puffinus", true)]
        [TestCase(null, false)]
        public void LatinNameIsRequired(String value, bool expectedPass)
        {
            var taxon = new Taxon { LatinName = value };
            var validationResult = ValidateModel(taxon);
            var result = !ValidationContainsKey(validationResult, "LatinName");
            Assert.AreEqual(expectedPass, result);
        }

        [Test]
        [TestCase("Awesome bird", true)]
        [TestCase(null, true)]
        public void DescriptionIsOptional(String value, bool expectedPass)
        {
            var taxon = new Taxon { Description = value };
            var validationResult = ValidateModel(taxon);
            var result = !ValidationContainsKey(validationResult, "Description");
            Assert.AreEqual(expectedPass, result);
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
