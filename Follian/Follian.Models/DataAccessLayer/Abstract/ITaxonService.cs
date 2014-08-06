using System;
using System.Collections.Generic;
using Foillan.Models.Biodiversity;

namespace Foillan.Models.DataAccessLayer.Abstract
{
    public interface ITaxonService
    {
        Taxon AddTaxon(Taxon newTaxon, Taxonomy taxonomy);
        IEnumerable<Taxon> GetTaxaByRank(TaxonRank rank);
        Taxon GetTaxonById(int id);
        Taxon GetTaxonByNameAndRank(string taxonLatinName, TaxonRank rank);
        void SaveChanges();
    }
}