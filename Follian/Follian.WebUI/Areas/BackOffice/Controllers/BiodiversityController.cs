using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.WebUI.Areas.BackOffice.Models;

namespace Foillan.WebUI.Areas.BackOffice.Controllers
{
    public class BiodiversityController : Controller
    {
        private readonly ITaxonService _taxonService;

        public BiodiversityController(ITaxonService taxonServiceIn)
        {
            _taxonService = taxonServiceIn;
        }

        public ViewResult Explore()
        {
            var viewModel = new ExplorerViewModel {Species = _taxonService.GetTaxaByRank(TaxonRank.Species)};
            return View("Explore", viewModel);
        }

        [HttpGet]
        public ActionResult AddSpecies()
        {
            var model = new AddSpeciesViewModel();
            return View("AddSpecies", model);
        }

        [HttpPost]
        public ActionResult AddSpecies(AddSpeciesViewModel newTaxon, HttpPostedFileBase file)
        {
            ModelState.Remove("Taxon.Id");
            ModelState.Remove("Taxon.ParentTaxon");

            if (!ModelState.IsValid)
            {
                return View("AddSpecies");
            }

            var taxonomy = GbifHelpers.GetTaxonomyDictionary(newTaxon.Taxon.GbifTaxonId);
            _taxonService.AddSpeciesWithHeirarchy(newTaxon.Taxon, taxonomy);
            //TODO Handle additional data

            _taxonService.SaveChanges();
            return RedirectToAction("Explore");
        }
    }
}