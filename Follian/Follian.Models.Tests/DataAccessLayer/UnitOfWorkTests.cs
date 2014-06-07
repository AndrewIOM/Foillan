using Foillan.Models.DataAccessLayer.Concrete;
using Moq;
using NUnit.Framework;

namespace Foillan.Models.Tests.DataAccessLayer
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [Test]
        public void UnitOfWork_DbContext_Get_ResultNotNull()
        {
            var mock = new Mock<FoillanContext>();
            var sut = new UnitOfWork(mock.Object);
            var result = sut.DbContext;
            Assert.NotNull(result);
            Assert.AreEqual(mock.Object, result, "The context returned does not match the created mock");
        }

        [Test]
        public void UnitOfWork_Save_CallsSaveOnDbContext()
        {
            var mock = new Mock<FoillanContext>();
            var sut = new UnitOfWork(mock.Object);
            sut.Save();
            mock.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}