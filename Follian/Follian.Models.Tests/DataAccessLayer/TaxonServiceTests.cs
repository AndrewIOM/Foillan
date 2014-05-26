using System;
using System.Linq;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Concrete;
using Foillan.Models.Tests.DummyClasses;
using Foillan.Models.Tests.TestBuilders;
using Moq;
using NUnit.Framework;

namespace Foillan.Models.Tests.DataAccessLayer
{
    [TestFixture]
    public class TaxonServiceTests
    {
        [Test]
        [TestCase(TaxonRank.Species)]
        [TestCase(TaxonRank.Genus)]
        [TestCase(TaxonRank.Subspecies)]
        public void TaxonService_GetTaxonOfRank_ReturnsTaxonOfRankFromRepository(TaxonRank rank)
        {
            var repository = new InMemoryTaxonRepository();
            var unitOfWork = new UnitOfWorkTestBuilder().Build();
            var sut = new TaxonService(unitOfWork, repository);

            var result = sut.GetTaxaByRank(rank).ToList();
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);

            var errors = (from taxon in result 
                          where taxon.Rank != rank 
                          select String.Format("Found error: {0} was of type {1}", taxon.LatinName, taxon.Rank)).ToList();

            Assert.IsEmpty(errors);
        }

        [Test]
        public void TaxonService_SaveChanges_TriggersSaveInUnitOfWork()
        {
            var unitOfWork = new Mock<DummyUnitOfWork>();
            var repository = new InMemoryTaxonRepository();
            var sut = new TaxonService(unitOfWork.Object, repository);

            sut.SaveChanges();
            unitOfWork.Verify(u => u.Save(), Times.Once);
        }
    }
}
