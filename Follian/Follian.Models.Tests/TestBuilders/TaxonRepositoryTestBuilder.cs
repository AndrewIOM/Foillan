using System.Collections.Generic;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Concrete;
using Moq;

namespace Foillan.Models.Tests.TestBuilders
{
    public class TaxonRepositoryTestBuilder
    {
        private readonly Mock<GenericRepository<Taxon>> _repository;
        private readonly List<Taxon> _taxaToReturn;

        public TaxonRepositoryTestBuilder()
        {
            var unitOfWork = new UnitOfWorkTestBuilder().Build();
            _repository = new Mock<GenericRepository<Taxon>>(unitOfWork);
            _taxaToReturn = new List<Taxon>();
        }

        public TaxonRepositoryTestBuilder ReturnSpecies()
        {
            var species = new Taxon()
            {
                Rank = TaxonRank.Species,
                ID = 1,
                LatinName = "puffinus"
            };

            species.Rank = TaxonRank.Species;
            _taxaToReturn.Add(species);
            return this;
        }

        public GenericRepository<Taxon> Build()
        {
            _repository.Setup(m => m.GetAll()).Returns(_taxaToReturn);
            return _repository.Object;
        } 
    }
}
