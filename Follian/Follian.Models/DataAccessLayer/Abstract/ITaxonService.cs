using System.Collections.Generic;
using Foillan.Models.Biodiversity;

namespace Foillan.Models.DataAccessLayer.Abstract
{
    public interface ITaxonService
    {
        IEnumerable<Taxon> GetTaxaByRank(TaxonRank rank);
        void AddTaxon(Taxon taxon);
        void SaveChanges();
    }
}