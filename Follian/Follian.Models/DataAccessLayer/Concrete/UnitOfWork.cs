using System;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models.DataAccessLayer.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private FoillanContext _context;

        public UnitOfWork(FoillanContext context)
        {
            _context = context;
        }

        public IFoillanContext DbContext
        {
            get { return _context ?? (_context = new FoillanContext()); }
        }

        public virtual int Save()
        {
            return _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposing || _context == null) return;
            _context.Dispose();
            _context = null;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
