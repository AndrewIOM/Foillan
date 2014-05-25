using Foillan.Models.DataAccessLayer.Concrete;
using NUnit.Framework;

namespace Foillan.Models.Tests.DataAccessLayer
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [Test]
        public void UnitOfWork_DbContext_Get_ResultNotNull()
        {
            var sut = new UnitOfWork();
            var result = sut.DbContext;
            Assert.IsNotNull(result);
        }

        [Test]
        public void UnitOfWork_Save_CallsSaveOnDbContext()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void UnitOfWork_DisposeWithNoParameters_DisposeOnDbContextCalled()
        {
            var sut = new UnitOfWork();
            sut.Dispose();
            Assert.Inconclusive();
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void UnitOfWork_Dispose_CallsDisposeOnDbContext(bool shouldDispose)
        {
            var sut = new UnitOfWork();
            sut.Dispose(shouldDispose);
            Assert.Inconclusive();
        }
    }
}
