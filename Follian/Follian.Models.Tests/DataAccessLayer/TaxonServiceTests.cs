using System;
using System.Collections.Generic;
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
        public void TaxonService_GetTaxonOfRank_OnlyReturnsTaxonOfRankFromRepository(TaxonRank rank)
        {
            var repository = new InMemoryTaxonRepository();
            var unitOfWork = new UnitOfWorkTestBuilder().Build();
            var sut = new TaxonService(unitOfWork, repository);

            var result = sut.GetTaxaByRank(rank).ToList();
            Assert.IsNotNull(result);
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

        [Test]
        public void TaxonService_AddSpeciesWithHeirarchy_DictionaryMissingSomeRanks_NoAdditions() 
        {
            var taxonomy = new Dictionary<TaxonRank, string> // Puffin
            {
                {TaxonRank.Kingdom, "Animalia"},
                {TaxonRank.Phylum, "Chordata"},
                //{TaxonRank.Class, "Aves"},
                {TaxonRank.Order, "Charadriiformes"},
                //{TaxonRank.Family, "Alcidae"},
                {TaxonRank.Genus, "Fratercula"}
            };
            var species = new Taxon()
            {
                Rank = TaxonRank.Species,
                LatinName = "arctica",
                GbifTaxonId = 4408612
            };
            var unitOfWork = new Mock<DummyUnitOfWork>();
            var repository = new InMemoryTaxonRepository();
            var sut = new TaxonService(unitOfWork.Object, repository);

            var expected = repository.GetAll();
            sut.AddSpeciesWithHeirarchy(species, taxonomy);

            Assert.AreEqual(expected, repository.GetAll(), 
                "Taxon were added to the database, although the taxonomy was invalid");
        }

        [Test]
        public void TaxonService_AddSpeciesWithHeirarchy_TaxonNotValidSpecies_NoAddition()
        {
            var taxonomy = new Dictionary<TaxonRank, string> // Puffin
            {
                {TaxonRank.Kingdom, "Animalia"},
                {TaxonRank.Phylum, "Chordata"},
                {TaxonRank.Class, "Aves"},
                {TaxonRank.Order, "Procellariiformes"},
                {TaxonRank.Family, "Procellariidae"},
            };
            var species = new Taxon()
            {
                Rank = TaxonRank.Genus,
                LatinName = "Invalid Species",
                GbifTaxonId = 4408612
            };
            var unitOfWork = new Mock<DummyUnitOfWork>();
            var repository = new InMemoryTaxonRepository();
            var sut = new TaxonService(unitOfWork.Object, repository);

            var expected = repository.GetAll();
            sut.AddSpeciesWithHeirarchy(species, taxonomy);

            Assert.AreEqual(expected, repository.GetAll(),
                "Taxon were added to the database, although the taxon was a Genus");
        }

        [Test]
        public void TaxonService_AddSpeciesWithHeirarchy_SpeciesAlreadyInDatabase_NoAdditions()
        {
            var taxonomy = new Dictionary<TaxonRank, string> // Puffin
            {
                {TaxonRank.Kingdom, "Animalia"},
                {TaxonRank.Phylum, "Chordata"},
                {TaxonRank.Class, "Aves"},
                {TaxonRank.Order, "Charadriiformes"},
                {TaxonRank.Family, "Alcidae"},
                {TaxonRank.Genus, "Fratercula"}
            };
            var species = new Taxon()
            {
                Rank = TaxonRank.Species,
                LatinName = "arctica",
                GbifTaxonId = 4408612
            };
            var unitOfWork = new Mock<DummyUnitOfWork>();
            var repository = new InMemoryTaxonRepository();
            repository.Add(species);
            var sut = new TaxonService(unitOfWork.Object, repository);

            var expected = repository.GetAll();
            sut.AddSpeciesWithHeirarchy(species, taxonomy);

            Assert.AreEqual(expected, repository.GetAll(),
                "Taxon were duplicated, as the species was already in the repository");
        }

        [Test]
        public void TaxonService_AddSpeciesWithHeirarchy_TaxonAndDictionaryValid_AddsSpecies()
        {
            var taxonomy = new Dictionary<TaxonRank, string> // Puffin
            {
                {TaxonRank.Kingdom, "Animalia"},
                {TaxonRank.Phylum, "Chordata"},
                {TaxonRank.Class, "Aves"},
                {TaxonRank.Order, "Charadriiformes"},
                {TaxonRank.Family, "Alcidae"},
                {TaxonRank.Genus, "Fratercula"}
            };
            var species = new Taxon()
            {
                Rank = TaxonRank.Species,
                LatinName = "arctica",
                GbifTaxonId = 4408612
            };
            var unitOfWork = new Mock<DummyUnitOfWork>();
            var repository = new InMemoryTaxonRepository();
            var sut = new TaxonService(unitOfWork.Object, repository);

            sut.AddSpeciesWithHeirarchy(species, taxonomy);
            var result = repository.GetAll().FirstOrDefault(t => t.LatinName.Equals("arctica"));

            Assert.IsTrue(result != null, "The species was not added to the database");
        }

        [Test]
        public void TaxonService_AddSpeciesWithHeirarchy_TaxonAndDictionaryValid_AddsAncestors()
        {
            var taxonomy = new Dictionary<TaxonRank, string>
            {
                {TaxonRank.Kingdom, "Animalia"},
                {TaxonRank.Phylum, "Chordata"},
                {TaxonRank.Class, "Aves"},
                {TaxonRank.Order, "Charadriiformes"},
                {TaxonRank.Family, "Alcidae"},
                {TaxonRank.Genus, "Fratercula"}
            };
            var species = new Taxon()
            {
                Rank = TaxonRank.Species,
                LatinName = "arctica",
                GbifTaxonId = 4408612
            };
            var unitOfWork = new Mock<DummyUnitOfWork>();
            var repository = new InMemoryTaxonRepository();
            var sut = new TaxonService(unitOfWork.Object, repository);

            sut.AddSpeciesWithHeirarchy(species, taxonomy);
            var results = repository.GetAll().ToList();
            var kingdomResult = results.FirstOrDefault(t => t.LatinName.Equals("Animalia"));
            var phylumResult = results.FirstOrDefault(t => t.LatinName.Equals("Chordata"));
            var classResult = results.FirstOrDefault(t => t.LatinName.Equals("Aves"));
            var orderResult = results.FirstOrDefault(t => t.LatinName.Equals("Charadriiformes"));
            var familyResult = results.FirstOrDefault(t => t.LatinName.Equals("Alcidae"));
            var genusResult = results.FirstOrDefault(t => t.LatinName.Equals("Fratercula") && t.Rank.Equals(TaxonRank.Genus));

            Assert.IsNotNull(kingdomResult, "Kingdom taxon was not added");
            Assert.IsNotNull(phylumResult, "Phylum taxon was not added");
            Assert.IsNotNull(classResult, "Class taxon was not added");
            Assert.IsNotNull(orderResult, "Order taxon was not added");
            Assert.IsNotNull(familyResult, "Family taxon was not added");
            Assert.IsNotNull(genusResult, "Genus taxon was not added");
        }

        [Test]
        public void TaxonService_AddSpeciesWithHeirarchy_TaxonAndDictionaryValid_ResultantTaxonomyHasCorrectAncestry()
        {
            var taxonomy = new Dictionary<TaxonRank, string>
            {
                {TaxonRank.Kingdom, "Animalia"},
                {TaxonRank.Phylum, "Chordata"},
                {TaxonRank.Class, "Aves"},
                {TaxonRank.Order, "Charadriiformes"},
                {TaxonRank.Family, "Alcidae"},
                {TaxonRank.Genus, "Fratercula"}
            };
            var species = new Taxon()
            {
                Rank = TaxonRank.Species,
                LatinName = "arctica",
                GbifTaxonId = 4408612
            };
            var unitOfWork = new Mock<DummyUnitOfWork>();
            var repository = new InMemoryTaxonRepository();
            var sut = new TaxonService(unitOfWork.Object, repository);

            sut.AddSpeciesWithHeirarchy(species, taxonomy);
            var results = repository.GetAll().ToList();

            var kingdomResult = results.FirstOrDefault(t => t.LatinName.Equals("Animalia"));
            var phylumResult = results.FirstOrDefault(t => t.LatinName.Equals("Chordata"));
            var classResult = results.FirstOrDefault(t => t.LatinName.Equals("Aves"));
            var orderResult = results.FirstOrDefault(t => t.LatinName.Equals("Charadriiformes"));
            var familyResult = results.FirstOrDefault(t => t.LatinName.Equals("Alcidae"));
            var genusResult = results.FirstOrDefault(t => t.LatinName.Equals("Fratercula")
                && t.Rank.Equals(TaxonRank.Genus));
            var speciesResult = results.FirstOrDefault(t => t.LatinName.Equals("arctica")
                && t.Rank.Equals(TaxonRank.Species));

            Assert.IsNull(kingdomResult.ParentTaxon, "Kingdom cannot have a parent taxon");
            Assert.IsTrue(phylumResult.ParentTaxon.Rank.Equals(TaxonRank.Kingdom), "Phylum should be linked to a kingdom");
            Assert.IsTrue(classResult.ParentTaxon.Rank.Equals(TaxonRank.Phylum), "Class should be linked to a parent phylum");
            Assert.IsTrue(orderResult.ParentTaxon.Rank.Equals(TaxonRank.Class), "Order should be linked to a parent class");
            Assert.IsTrue(familyResult.ParentTaxon.Rank.Equals(TaxonRank.Order), "Family should be linked to a parent order");
            Assert.IsTrue(genusResult.ParentTaxon.Rank.Equals(TaxonRank.Family), "Genus should be linked to a parent family");
            Assert.IsTrue(speciesResult.ParentTaxon.Rank.Equals(TaxonRank.Genus), "Species should be linked to a parent genus");
        }

    }
}
