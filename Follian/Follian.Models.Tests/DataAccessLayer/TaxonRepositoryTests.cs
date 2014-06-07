using System.Data.Entity;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.Models.DataAccessLayer.Concrete;
using Foillan.Models.Tests.DummyClasses;
using Foillan.Models.Tests.TestBuilders;
using Moq;
using NUnit.Framework;

namespace Foillan.Models.Tests.DataAccessLayer
{
    [TestFixture]
    public class TaxonRepositoryTests
    {
        //[Test]
        //public void TaxonRepository_GetById_IdInDatabase_ReturnsTaxonObject()
        //{
        //    var expected = new Taxon { Id = 1, LatinName = "Fake Taxon" };
        //    var dummyUnitOfWork = new DummyUnitOfWork();
        //    var sut = new TaxonRepository(dummyUnitOfWork);

        //    var result = sut.GetById(1);

        //    Assert.NotNull(result);
        //    Assert.AreEqual(expected, result);
        //}

        //[Test]
        //public void TaxonRepository_GetById_IdNotInDatabase_ReturnsNull()
        //{
        //    Assert.Inconclusive();
        //}

        //[Test]
        //public void TaxonRepository_Add_IdAlreadyInDatabase_DatabaseNotUpdated()
        //{
        //    Assert.Inconclusive();
        //}

        //[Test]
        //public void TaxonRepository_Add_ValidTaxon_DatabaseUpdatedWithTaxon()
        //{
        //    var fakeUnitOfWork = new UnitOfWorkTestBuilder().BuildMock();
        //    var sut = new TaxonRepository(fakeUnitOfWork.Object);

        //    Assert.Inconclusive();
        //}
    }
}
