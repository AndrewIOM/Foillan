using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.Models.Occurrence;

namespace Foillan.Models.Biodiversity
{
    public class SpeciesDetails : IEntity<int>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [UIHint("SingleImageUpload")]
        public string Image { get; set; }

        public virtual IEnumerable<AlternativeName> AlternativeNames { get; set; }
        public virtual IEnumerable<Sighting> Sightings { get; set; }

        public virtual Taxon Taxon { get; set; }
    }
}