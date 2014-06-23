using Foillan.Models.Biodiversity;

namespace Foillan.WebService.ViewModels
{
    public class AddSpeciesViewModel
    {
        public Taxon Taxon { get; set; }
        public SpeciesDetails AdditionalDetails { get; set; }
    }
}