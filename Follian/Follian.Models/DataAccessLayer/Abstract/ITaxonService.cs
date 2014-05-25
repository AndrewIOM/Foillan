using System.Collections.Generic;
using Follian.Models.Biodiversity;

namespace Follian.Models.DataAccessLayer.Abstract
{
    public interface ITaxonService
    {
        IEnumerable<Taxon> GetTaxaByRank(TaxonRank rank);
        void SaveChanges();
    }
}