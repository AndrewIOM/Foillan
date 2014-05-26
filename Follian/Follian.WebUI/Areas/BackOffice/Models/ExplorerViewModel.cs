using System.Collections.Generic;
using Foillan.Models.Biodiversity;

namespace Foillan.WebUI.Areas.BackOffice.Models
{
    public class ExplorerViewModel
    {
        public IEnumerable<Taxon> Species { get; set; }
    }
}