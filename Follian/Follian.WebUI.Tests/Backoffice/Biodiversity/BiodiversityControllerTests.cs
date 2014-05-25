using System.Linq;
using System.Web.Mvc;
using Follian.Models.Tests.TestBuilders;
using Follian.WebUI.Areas.BackOffice.Controllers;
using Follian.WebUI.Models;
using NUnit.Framework;

namespace Follian.WebUI.Tests.Backoffice.Biodiversity
{
    [TestFixture]
    public class BiodiversityControllerTests
    {
        [Test]
        public void Species_ReturnsSpeciesView()
        {
            var taxonService = new TaxonServiceTestBuilder().Build();
            var sut = new BiodiversityController(taxonService);
            var result = sut.Species() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreSame("Species", result.ViewName);
        }

        [Test]
        public void Species_ViewContainsSpeciesViewModel()
        {
            var taxonService = new TaxonServiceTestBuilder().Build();
            var sut = new BiodiversityController(taxonService);
            var result = sut.Species() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<SpeciesExplorerViewModel>(result.ViewData.Model);
        }

        [Test]
        public void Species_ViewModelPopulatedWithSpecies()
        {
            var taxonService = new TaxonServiceTestBuilder().ReturnsSpecies().Build();
            var sut = new BiodiversityController(taxonService);
            var result = sut.Species() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as SpeciesExplorerViewModel;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Species);
            Assert.IsTrue(model.Species.Any());
        }
    }
}
