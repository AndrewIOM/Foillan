using System.Collections.Generic;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Concrete;
using Moq;

namespace Foillan.Models.Tests.TestBuilders
{
    public class TaxonServiceTestBuilder
    {
        private readonly Mock<TaxonService> _service;

        public TaxonServiceTestBuilder()
        {
            var unitOfWork = new UnitOfWork(new Mock<FoillanContext>().Object);
            var taxonRepository = new TaxonRepository(unitOfWork);
            _service = new Mock<TaxonService>(unitOfWork, taxonRepository);
        }

        public TaxonServiceTestBuilder ReturnsSpecies()
        {
            var species = new Taxon()
            {
                Rank = TaxonRank.Species,
                Id = 1,
                LatinName = "puffinus"
            };

            var speciesList = new List<Taxon> {species};
            _service.Setup(m => m.GetTaxaByRank(TaxonRank.Species)).Returns(speciesList);
            return this;
        }

        public TaxonService Build()
        {
            return _service.Object;
        }
    }
}