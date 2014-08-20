using Foillan.Models.DataAccess.Abstract;
using Moq;

namespace Foillan.Models.Tests.TestBuilders
{
    public class FoillanContextTestBuilder
    {
        private Mock<IFoillanContext> _context;

        public FoillanContextTestBuilder()
        {
            _context = new Mock<IFoillanContext>();
        }

        public IFoillanContext Build()
        {
            return _context.Object;
        }
    }
}
