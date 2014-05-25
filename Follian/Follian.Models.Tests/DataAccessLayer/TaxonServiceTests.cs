using System;
using System.Linq;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Concrete;
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
        public void TaxonService_GetTaxonOfRank_ReturnsTaxonOfRankFromRepository(TaxonRank rank)
        {
            var unitOfWork = new Mock<UnitOfWork>();
            var repository = new TaxonRepositoryTestBuilder().ReturnSpecies().Build();
            var sut = new TaxonService(unitOfWork.Object, repository);

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
            var unitOfWork = new Mock<UnitOfWork>();
            var repository = new GenericRepository<Taxon>(unitOfWork.Object);
            var sut = new TaxonService(unitOfWork.Object, repository);

            sut.SaveChanges();
            unitOfWork.Verify(u => u.Save(), Times.Once);
        }
    }
}
