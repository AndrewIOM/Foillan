using Foillan.Models.DataAccessLayer.Concrete;
using Moq;

namespace Foillan.Models.Tests.TestBuilders
{
    public class UnitOfWorkTestBuilder
    {
        private readonly Mock<UnitOfWork> _mock;

        public UnitOfWorkTestBuilder()
        {
            _mock = new Mock<UnitOfWork>();
        }

        public UnitOfWork Build()
        {
            return _mock.Object;
        }
    }
}
