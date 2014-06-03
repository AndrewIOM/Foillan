using System.Collections.Generic;

namespace Foillan.Models.Biodiversity
{
    public class GbifSpeciesResponse
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public bool endOfRecords { get; set; }
        public List<Result> results { get; set; }
    }

    public class Identifier
    {
        public int key { get; set; }
        public int usageKey { get; set; }
        public string datasetKey { get; set; }
        public string identifier { get; set; }
        public string type { get; set; }
    }

    public class Result
    {
        public int key { get; set; }
        public string kingdom { get; set; }
        public string phylum { get; set; }
        public string clazz { get; set; }
        public string order { get; set; }
        public string family { get; set; }
        public string genus { get; set; }
        public int kingdomKey { get; set; }
        public int phylumKey { get; set; }
        public int classKey { get; set; }
        public int orderKey { get; set; }
        public int familyKey { get; set; }
        public int genusKey { get; set; }
        public string datasetKey { get; set; }
        public int nubKey { get; set; }
        public int parentKey { get; set; }
        public string parent { get; set; }
        public string scientificName { get; set; }
        public string canonicalName { get; set; }
        public string authorship { get; set; }
        public string nameType { get; set; }
        public string rank { get; set; }
        public string origin { get; set; }
        public string taxonomicStatus { get; set; }
        public List<object> nomenclaturalStatus { get; set; }
        public string publishedIn { get; set; }
        public string accordingTo { get; set; }
        public int numDescendants { get; set; }
        public List<Identifier> identifiers { get; set; }
        public string sourceId { get; set; }
        public bool synonym { get; set; }
        public string remarks { get; set; }
        public string link { get; set; }
    }
}
