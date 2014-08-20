using System.Linq;
using Foillan.Models.Occurrence;

namespace Foillan.Models.DataAccess.Abstract
{
    public interface ISightingService
    {
        Sighting AddSighting(Sighting newSighting);
        IQueryable<Sighting> GetAll();
        Sighting GetSighting(int id);
        void SaveChanges();
    }
}