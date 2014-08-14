using System.Collections.Generic;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Concrete;
using Foillan.Models.Tests.DummyClasses;
using Moq;

namespace Foillan.Models.Tests.TestBuilders
{
    public class TaxonServiceTestBuilder
    {
        private readonly Mock<TaxonService> _service;

        public TaxonServiceTestBuilder()
        {
            var unitOfWork = new DummyUnitOfWork();
            var taxonRepository = new InMemoryTaxonRepository();
            _service = new Mock<TaxonService>(unitOfWork, taxonRepository);
        }

        public TaxonServiceTestBuilder ReturnsSpecies()
        {
            var species1 = new Taxon
            {
                Rank = TaxonRank.Species,
                Id = 1,
                LatinName = "Puffinus puffinus"
            };

            var species2 = new Taxon
            {
                Rank = TaxonRank.Species,
                Id = 1,
                LatinName = "Abies alba"
            };

            var species3 = new Taxon
            {
                Rank = TaxonRank.Species,
                Id = 1,
                LatinName = "Fraxinus Excelsior"
            };

            var speciesList = new List<Taxon> {species1, species2, species3};
            _service.Setup(m => m.GetTaxaByRank(TaxonRank.Species)).Returns(speciesList);
            return this;
        }

        public TaxonServiceTestBuilder ReturnsTaxonOfId(int id, Taxon taxon)
        {
            _service.Setup(m => m.GetTaxonById(id)).Returns(taxon);
            return this;
        }

        public TaxonService Build()
        {
            return _service.Object;
        }

        public Mock<TaxonService> BuildMock()
        {
            return _service;
        }
    }
}