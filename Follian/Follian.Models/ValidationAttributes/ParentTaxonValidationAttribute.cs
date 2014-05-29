using System;
using System.ComponentModel.DataAnnotations;
using Foillan.Models.Biodiversity;

namespace Foillan.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ParentTaxonValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var taxon = value as Taxon;
            if (taxon == null)
            {
                return false;
            }
            if (taxon.ParentTaxon == null)
            {
                return false;
            }
            if (taxon.ParentTaxon.Rank == TaxonRank.Null)
            {
                return true;
            }

            var parentRank = taxon.ParentTaxon.Rank;
            var taxonRank = taxon.Rank;
            return taxonRank == (parentRank + 1);
        }

    }
}
