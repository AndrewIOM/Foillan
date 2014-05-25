using System.Collections.Generic;
using Follian.Models.Biodiversity;
using Follian.Models.DataAccessLayer.Concrete;
using Moq;
using NUnit.Framework;

namespace Follian.Models.Tests.TestBuilders
{
    public class TaxonServiceTestBuilder
    {
        private readonly Mock<TaxonService> _service;

        public TaxonServiceTestBuilder()
        {
            var unitOfWork = new UnitOfWork();
            var taxonRepository = new GenericRepository<Taxon>(unitOfWork);
            _service = new Mock<TaxonService>(unitOfWork, taxonRepository);
        }

        public TaxonServiceTestBuilder ReturnsSpecies()
        {
            var species = new Taxon()
            {
                Rank = TaxonRank.Species,
                ID = 1,
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