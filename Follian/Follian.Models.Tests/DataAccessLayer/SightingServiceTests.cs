using System;
using System.Collections.Generic;
using System.Linq;
using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.Models.DataAccessLayer.Concrete;
using Foillan.Models.Occurrence;
using Foillan.Models.Tests.DummyClasses;
using Moq;
using NUnit.Framework;

namespace Foillan.Models.Tests.DataAccessLayer
{
    [TestFixture]
    public class SightingServiceTests
    {
        DummyUnitOfWork _unitOfWork;
        Mock<IRepository<Sighting>> _repository;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new DummyUnitOfWork();
            _repository = new Mock<IRepository<Sighting>>();
        }

        [Test]
        public void AddSighting_SightingNull_ThrowsArgumentExceptionAndNoAdditionsToRepository()
        {
            var sut = new SightingService(_unitOfWork, _repository.Object);
            Assert.Throws<ArgumentException>(() => sut.AddSighting(null));
            _repository.Verify(m => m.Add(It.IsAny<Sighting>()), Times.Never);
        }

        [Test]
        public void AddSighting_SightingValid_AddsToRepositoryExactlyOnce()
        {
            var sighting = new Sighting();
            var sut = new SightingService(_unitOfWork, _repository.Object);
            sut.AddSighting(sighting);
            _repository.Verify(m => m.Add(It.IsAny<Sighting>()), Times.Once);
        }

        [Test]
        public void AddSighting_SightingValid_ReturnsSightingWithId()
        {
            var sighting = new Sighting();
            var expected = sighting;
            expected.Id = 43;
            _repository.Setup(m => m.Add(sighting)).Returns(expected);
            var sut = new SightingService(_unitOfWork, _repository.Object);

            var result = sut.AddSighting(sighting);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAll_RetrievesAllFromRepositoryOnce()
        {
            var sut = new SightingService(_unitOfWork, _repository.Object);
            sut.GetAll();
            _repository.Verify(m => m.GetAll(), Times.Once);
        }

        [Test]
        public void GetAll_ReturnsListOfRepositoryItems()
        {
            _repository.Setup(m => m.GetAll()).Returns(new List<Sighting> {new Sighting(), new Sighting()});
            var sut = new SightingService(_unitOfWork, _repository.Object);
            var result = sut.GetAll();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public void GetSighting_RetrivedFromRepositoryOnce()
        {
            var sut = new SightingService(_unitOfWork, _repository.Object);
            sut.GetSighting(6);
            _repository.Verify(m => m.GetById(6), Times.Once);
        }

        [Test]
        public void GetTaxonById_TaxonWithIdNotInDatabase_ReturnsNull()
        {
            var sut = new SightingService(_unitOfWork, _repository.Object);
            var result = sut.GetSighting(6);
            Assert.IsNull(result);
        }

        [Test]
        public void GetTaxonById_TaxonInDatabase_ReturnsTaxon()
        {
            _repository.Setup(m => m.GetById(6)).Returns(new Sighting {Id = 6});
            var sut = new SightingService(_unitOfWork, _repository.Object);
            var result = sut.GetSighting(6);
            Assert.IsNotNull(result);
        }

        [Test]
        public void SaveChanges_SavesUnitOfWorkOnce()
        {
            var unit = new Mock<IUnitOfWork>();
            var sut = new SightingService(unit.Object, _repository.Object);
            sut.SaveChanges();
            unit.Verify(m => m.Save(), Times.Once);
        }
    }
}
