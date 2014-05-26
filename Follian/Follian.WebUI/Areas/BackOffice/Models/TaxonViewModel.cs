using Foillan.Models.Biodiversity;

namespace Foillan.WebUI.Areas.BackOffice.Models
{
    public class TaxonViewModel
    {
        public Taxon Taxon { get; set; }
        public TaxonRank Rank { get; set; }
        public SpeciesDetails AdditionalDetails { get; set; }
    }
}