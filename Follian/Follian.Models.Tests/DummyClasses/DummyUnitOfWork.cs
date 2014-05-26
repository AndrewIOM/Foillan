using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;
using NUnit.Framework;

namespace Foillan.Models.Tests.DummyClasses
{
    public class DummyUnitOfWork : IUnitOfWork
    {
        public IFoillanContext DbContext { get; set; }
        public virtual int Save()
        {
            return 1;
        }
    }
}
