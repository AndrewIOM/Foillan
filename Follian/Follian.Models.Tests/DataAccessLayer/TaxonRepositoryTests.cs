﻿using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Concrete;
using Foillan.Models.Tests.TestBuilders;
using NUnit.Framework;

namespace Foillan.Models.Tests.DataAccessLayer
{
    [TestFixture]
    public class TaxonRepositoryTests
    {
        //[Test]
        //public void TaxonRepository_GetById_IdInDatabase_ReturnsTaxonObject()
        //{
        //    var expected = new Taxon {Id = 1, LatinName = "Fake Taxon"};
        //    var fakeUnitOfWork = new UnitOfWorkTestBuilder().HasTaxon(expected).BuildMock();
        //    var sut = new TaxonRepository(fakeUnitOfWork.Object);
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