using System.Collections.Generic;
using Foillan.Models.Biodiversity;

namespace Foillan.WebService.ViewModels
{
    public class ExplorerViewModel
    {
        public IEnumerable<Taxon> Species { get; set; }
    }
}