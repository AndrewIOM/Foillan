using System.Web;
using System.Web.Mvc;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.Models.Utilities;
using Foillan.WebService.ViewModels;

namespace Foillan.WebService.Controllers
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

            var taxonomy = GbifHelper.GetTaxonomyDictionary(newTaxon.Taxon.GbifTaxonId);
            _taxonService.AddSpeciesWithHeirarchy(newTaxon.Taxon, taxonomy);
            //TODO Handle additional data

            _taxonService.SaveChanges();
            return RedirectToAction("Explore");
        }

        [HttpGet]
        public ActionResult AddSpeciesBatch()
        {
            return View("AddSpeciesBatch");
        }

        [HttpPost]
        public ActionResult AddSpeciesBatch(HttpPostedFileBase batch)
        {
            if (batch == null)
            {
                ModelState.AddModelError("file", "The file upload was empty");
                return View("AddSpeciesBatch");
            }

            if (batch.ContentLength <= 0)
            {
                ModelState.AddModelError("file", "The file upload was empty");
                return View("AddSpeciesBatch");
            }

            var speciesFromFile = CommaSeperatedFileUtility.GetSpeciesFromCommaSeperatedFile(batch.InputStream);
            foreach (var species in speciesFromFile)
            {
                var gbifIdentifier = GbifHelper.GetGbifIdForTaxon(species.LatinName, species.Rank);
                species.GbifTaxonId = gbifIdentifier;
                 var taxonomy = GbifHelper.GetTaxonomyDictionary(gbifIdentifier);
                _taxonService.AddSpeciesWithHeirarchy(species, taxonomy);
            }

            return View("AddSpeciesBatch");
        }
    }
}