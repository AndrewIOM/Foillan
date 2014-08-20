using System.Collections.Generic;
using System.Linq;
using Foillan.Models.Occurrence;

namespace Foillan.Models.DataAccessLayer.Abstract
{
    public interface ISightingService
    {
        Sighting AddSighting(Sighting newSighting);
        IQueryable<Sighting> GetAll();
        Sighting GetSighting(int id);
        void SaveChanges();
    }
}