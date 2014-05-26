using System.Collections.Generic;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Concrete;
using Moq;

namespace Foillan.Models.Tests.TestBuilders
{
    public class TaxonRepositoryTestBuilder
    {
        private readonly Mock<TaxonRepository> _repository;
        private readonly List<Taxon> _taxaToReturn;

        public TaxonRepositoryTestBuilder()
        {
            var unitOfWork = new UnitOfWorkTestBuilder().Build();
            _repository = new Mock<TaxonRepository>(unitOfWork);
            _taxaToReturn = new List<Taxon>();
        }

        public TaxonRepositoryTestBuilder ReturnSpecies()
        {
            var species = new Taxon()
            {
                Rank = TaxonRank.Species,
                Id = 1,
                LatinName = "puffinus"
            };

            species.Rank = TaxonRank.Species;
            _taxaToReturn.Add(species);
            return this;
        }

        public TaxonRepository Build()
        {
            _repository.Setup(m => m.GetAll()).Returns(_taxaToReturn);
            return _repository.Object;
        }

        public Mock<TaxonRepository> BuildMock()
        {
            _repository.Setup(m => m.GetAll()).Returns(_taxaToReturn);
            return _repository;
        }
    }
}
