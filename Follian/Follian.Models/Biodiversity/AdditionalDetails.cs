using System.Collections.Generic;
using System.Web.Mvc;
using Foillan.Models.DataAccess.Abstract;
using Foillan.Models.Occurrence;

namespace Foillan.Models.Biodiversity
{
    public class AdditionalDetails : IEntity<int>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string Image { get; set; }

        public virtual IEnumerable<AdditionalName> AlternativeNames { get; set; }
        public virtual IEnumerable<Sighting> Sightings { get; set; }

        public virtual Taxon Taxon { get; set; }
    }
}