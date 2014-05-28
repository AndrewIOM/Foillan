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
            return false;
        }

    }
}
