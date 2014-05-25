using System.Web.Mvc;
using Follian.Models.Biodiversity;
using Follian.Models.DataAccessLayer.Abstract;
using Follian.WebUI.Models;

namespace Follian.WebUI.Areas.BackOffice.Controllers
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