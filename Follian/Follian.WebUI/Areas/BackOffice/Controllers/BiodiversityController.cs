using System.Web.Mvc;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.WebUI.Models;

namespace Foillan.WebUI.Areas.BackOffice.Controllers
{
    public class BiodiversityController : Controller
    {
        private readonly ITaxonService _taxonService;

        public BiodiversityController(ITaxonService taxonServiceIn)
        {
            _taxonService = taxonServiceIn;
        }

        public ViewResult Species()
        {
            var viewModel = new SpeciesExplorerViewModel {Species = _taxonService.GetTaxaByRank(TaxonRank.Species)};
            return View("Species", viewModel);
        }
    }
}