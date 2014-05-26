using System.Collections.Generic;
using System.Data.Entity;
using Foillan.Models.Biodiversity;
using Foillan.Models.Tests.DummyClasses;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Foillan.Models.Tests.TestBuilders
{
    public class UnitOfWorkTestBuilder
    {
        private readonly Mock<DummyUnitOfWork> _mock;
        private readonly IEnumerable<Taxon> _taxaInDb;

        public UnitOfWorkTestBuilder()
        {
            _mock = new Mock<DummyUnitOfWork>();
            _taxaInDb = new List<Taxon>();
        }

        public UnitOfWorkTestBuilder SavesOneItem()
        {
            _mock.Setup(m => m.Save()).Returns(1);
            return this;
        }

        public UnitOfWorkTestBuilder HasTaxon(Taxon taxon)
        {
            return this;
        }

        public DummyUnitOfWork Build()
        {
            return _mock.Object;
        }

        public Mock<DummyUnitOfWork> BuildMock()
        {
            return _mock;
        }
    }
}