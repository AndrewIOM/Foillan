using System;
using System.Collections.Generic;
using Foillan.Models.Biodiversity;

namespace Foillan.Models.DataAccessLayer.Abstract
{
    public interface ITaxonService
    {
        IEnumerable<Taxon> GetTaxaByRank(TaxonRank rank);
        Taxon AddTaxon(Taxon taxon);
        Taxon AddSpeciesWithHeirarchy(Taxon species, IDictionary<TaxonRank, String> heirarchyDictionary);
        void SaveChanges();
    }
}