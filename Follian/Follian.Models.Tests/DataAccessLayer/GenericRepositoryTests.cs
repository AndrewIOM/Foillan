using NUnit.Framework;

namespace Foillan.Models.Tests.DataAccessLayer
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        [Test]
        public void GenericRepository_Add_EntityHasNullId_NoChangesToDatabase()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void GenericRepository_Add_ValidId_EntitySavedToDatabase()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void GenericRepository_Update_EntityHasNullId_NoChangesToDatabase()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void GenericRepository_Update_EntityIdenticalToEntityInDatabase_NoChangesToDatabase()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void GenericRepository_Update_EntityDiffersToEntityInDatabase_DatabaseUpdated()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void GenericRepository_Delete_EntityNotInDatabase_NoChangesToDatabase()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void GenericRepository_Delete_EntityIdNull_NoChangesToDatabase()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void GenericRepository_Delete_EntityIdInDatabase_EntityRemovedFromDatabase()
        {
            Assert.Inconclusive();

        }

        [Test]
        public void GenericRepository_GetById_IdInDatabase_ReturnsEntity()
        {
            Assert.Inconclusive();

        }

        [Test]
        public void GenericRepository_GetById_IdNotInDatabase_ReturnsNull()
        {
            Assert.Inconclusive();

        }

        [Test]
        public void GenericRepository_GetAll_ReturnedEntitiesMatchThoseInDatabase()
        {
            Assert.Inconclusive();

        }

    }
}
