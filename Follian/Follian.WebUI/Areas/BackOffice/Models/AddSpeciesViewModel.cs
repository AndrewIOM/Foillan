using Foillan.Models.Biodiversity;

namespace Foillan.WebUI.Areas.BackOffice.Models
{
    public class AddSpeciesViewModel
    {
        public Taxon Taxon { get; set; }
        public SpeciesDetails AdditionalDetails { get; set; }
    }
}