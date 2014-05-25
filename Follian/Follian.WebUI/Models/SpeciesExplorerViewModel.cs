using System.Collections.Generic;
using Follian.Models.Biodiversity;

namespace Follian.WebUI.Models
{
    public class SpeciesExplorerViewModel
    {
        public IEnumerable<Taxon> Species { get; set; }
    }
}