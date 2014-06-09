﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Foillan.Models.Biodiversity;
using Foillan.Models.Tests.TestBuilders;
using Foillan.WebUI.Areas.BackOffice.Controllers;
using Foillan.WebUI.Areas.BackOffice.Models;
using Moq;
using NUnit.Framework;

namespace Foillan.WebUI.Tests.Backoffice.Biodiversity
{
    [TestFixture]
    public class BiodiversityControllerTests
    {
        [Test]
        public void Species_ReturnsSpeciesView()
        {
            var taxonService = new TaxonServiceTestBuilder().Build();
            var sut = new BiodiversityController(taxonService);
            var result = sut.Explore() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreSame("Explore", result.ViewName);
        }

        [Test]
        public void Species_ViewContainsSpeciesViewModel()
        {
            var taxonService = new TaxonServiceTestBuilder().Build();
            var sut = new BiodiversityController(taxonService);
            var result = sut.Explore() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ExplorerViewModel>(result.ViewData.Model);
        }

        [Test]
        public void Species_ViewModelPopulatedWithSpecies()
        {
            var taxonService = new TaxonServiceTestBuilder().ReturnsSpecies().Build();
            var sut = new BiodiversityController(taxonService);
            var result = sut.Explore() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as ExplorerViewModel;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Species);
            Assert.IsTrue(model.Species.Any());
        }

        [Test]
        public void AddTaxon_ReturnsAddTaxonView()
        {
            var taxonService = new TaxonServiceTestBuilder().ReturnsSpecies().Build();
            var sut = new BiodiversityController(taxonService);
            var result = sut.AddSpecies() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("AddSpecies", result.ViewName);
        }

        [Test]
        public void AddTaxon_ViewModelIsOfTypeTaxon()
        {
            var taxonService = new TaxonServiceTestBuilder().ReturnsSpecies().Build();
            var sut = new BiodiversityController(taxonService);
            var result = sut.AddSpecies() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as AddSpeciesViewModel;
            Assert.IsNotNull(model);
        }

        [Test]
        public void AddTaxon_HttpPost_ValidModel_DatabaseSaved()
        {
            var taxonService = new TaxonServiceTestBuilder().BuildMock();
            var sut = new BiodiversityController(taxonService.Object);
            var model = new AddSpeciesViewModel {Taxon = new Taxon {Id = 1, LatinName = "Test Taxon"}};
            sut.AddSpecies(model, null);
            taxonService.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void AddTaxon_HttpPost_ValidModel_RedirectedToExploreView()
        {
            var taxonService = new TaxonServiceTestBuilder().BuildMock();
            var sut = new BiodiversityController(taxonService.Object);
            var model = new AddSpeciesViewModel { Taxon = new Taxon { Id = 1, LatinName = "Test Taxon" } };
            var result = sut.AddSpecies(model, null) as ViewResult;
            Assert.NotNull(result);
            Assert.AreEqual("Explore", result.ViewName);
        }

        [Test]
        public void AddTaxon_HttpPost_ModelHasValidationErrors_ReturnsToAddTaxonView()
        {
            var taxonService = new TaxonServiceTestBuilder().BuildMock();
            var sut = new BiodiversityController(taxonService.Object);
            var model = new AddSpeciesViewModel { Taxon = new Taxon { Id = 1, LatinName = "Test Taxon" } };
            sut.ModelState.AddModelError("Test Error", new Exception());
            var result = sut.AddSpecies(model, null) as ViewResult;
            Assert.NotNull(result);
            Assert.AreEqual("AddSpecies", result.ViewName);
        }

        [Test]
        public void AddTaxon_HttpPost_ValidModel_TaxonAddedUsingTaxonService()
        {
            var taxonService = new TaxonServiceTestBuilder().BuildMock();
            var sut = new BiodiversityController(taxonService.Object);
            var model = new AddSpeciesViewModel { Taxon = new Taxon { GbifTaxonId = 4408612, LatinName = "arctica" } };
            sut.AddSpecies(model, null);
            taxonService.Verify(m => m.AddSpeciesWithHeirarchy(model.Taxon, 
                It.IsAny<Dictionary<TaxonRank, String>>()), Times.Once());
        }

    }
}
