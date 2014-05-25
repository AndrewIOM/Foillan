using System;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models.DataAccessLayer.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private BiodiversityDbContext _context;

        public BiodiversityDbContext DbContext
        {
            get { return _context ?? (_context = new BiodiversityDbContext()); }
        }

        public virtual int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (!disposing || _context == null) return;
            _context.Dispose();
            _context = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
