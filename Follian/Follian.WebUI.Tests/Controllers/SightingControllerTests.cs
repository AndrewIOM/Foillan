using System;
using System.Web.Mvc;
using Foillan.Models;
using Foillan.WebUI.Controllers;
using NUnit.Framework;

namespace Foillan.WebUI.Tests.Controllers
{
    [TestFixture]
    public class SightingControllerTests
    {
        private const String SIGHTINGS_FORM_VIEW_NAME = "Sighting";
        private const String SUCCESS_VIEW_NAME = "Success";

        private const String NULL_VIEW_ERROR = "The controller method did not return a view";

        [Test]
        public void Sighting_ReturnsSightingDataEntryForm()
        {
            var Sut = new SightingController();
            var Result = Sut.Sighting() as ViewResult;
            Assert.IsNotNull(Result, NULL_VIEW_ERROR);
            Assert.AreSame(SIGHTINGS_FORM_VIEW_NAME, Result.ViewName);
        }

        [Test]
        public void Sighting_ViewContainsNewSightingsModel()
        {
            var Sut = new SightingController();
            var Result = Sut.Sighting() as ViewResult;
            Assert.IsNotNull(Result);
            Assert.IsInstanceOf<Sighting>(Result.ViewData.Model);
        }

        [Test]
        public void Sighting_HttpPost_ModelStateValid_ReturnsSuccessView()
        {
            var Sut = new SightingController();
            var Model = new Sighting();
            var Result = Sut.Sighting(Model) as ViewResult;
            Assert.IsNotNull(Result, NULL_VIEW_ERROR);
            Assert.AreEqual(SUCCESS_VIEW_NAME, Result.ViewName);
        }

        [Test]
        public void Sighting_HttpPost_ModelStateInvalid_ReturnsToSightingsFormView()
        {
            var Sut = new SightingController();
            var Model = new Sighting();
            Sut.ModelState.AddModelError("FakeError", "This is a fake error for unit testing");
            var Result = Sut.Sighting(Model) as ViewResult;
            Assert.IsNotNull(Result, NULL_VIEW_ERROR);
            Assert.AreEqual(SIGHTINGS_FORM_VIEW_NAME, Result.ViewName);
        }

    }
}
