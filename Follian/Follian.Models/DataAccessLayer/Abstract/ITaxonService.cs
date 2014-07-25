using System;
using System.Collections.Generic;
using Foillan.Models.Biodiversity;

namespace Foillan.Models.DataAccessLayer.Abstract
{
    public interface ITaxonService
    {
        Taxon AddTaxonWithTaxonomy(Taxon newTaxon, IDictionary<TaxonRank, string> taxonomy);
        IEnumerable<Taxon> GetTaxaByRank(TaxonRank rank);
        Taxon GetTaxonById(int id);
        Taxon GetTaxonByNameAndRank(string taxonLatinName, TaxonRank rank);
        void SaveChanges();
    }
}