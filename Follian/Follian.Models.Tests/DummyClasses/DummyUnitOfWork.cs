using Foillan.Models.DataAccessLayer.Abstract;
using Foillan.Models.Tests.TestBuilders;

namespace Foillan.Models.Tests.DummyClasses
{
    public class DummyUnitOfWork : IUnitOfWork
    {
        public DummyUnitOfWork()
        {
            DbContext = new FoillanContextTestBuilder().Build();
        }

        public IFoillanContext DbContext { get; set; }
        public virtual int Save()
        {
            return 1;
        }
    }
}
