﻿using System.Web;
using System.Web.Mvc;
using Foillan.Models.Biodiversity;
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
        public ActionResult AddTaxon()
        {
            var model = new TaxonViewModel();
            return View("AddTaxon", model);
        }

        [HttpPost]
        public ActionResult AddTaxon(TaxonViewModel newTaxon, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View("AddTaxon");
            }

            _taxonService.AddTaxon(newTaxon.Taxon);
            _taxonService.SaveChanges();
            return Explore();
        }
    }
}