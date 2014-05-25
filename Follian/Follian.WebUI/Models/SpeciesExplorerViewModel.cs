using System.Collections.Generic;
using Foillan.Models.Biodiversity;

namespace Foillan.WebUI.Models
{
    public class SpeciesExplorerViewModel
    {
        public IEnumerable<Taxon> Species { get; set; }
    }
}