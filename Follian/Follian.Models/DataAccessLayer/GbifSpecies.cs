using System.Runtime.Serialization;

namespace Foillan.Models.DataAccessLayer
{
    [DataContract]
    public class GbifSpecies
    {
        [DataMember(Name = "usageKey")]
        public int UsageKey { get; set; }
        [DataMember(Name = "key")]
        public int Key { get; set; }
        [DataMember(Name = "scientificName")]
        public string ScientificName { get; set; }
        [DataMember(Name = "canonicalName")]
        public string CanonicalName { get; set; }
        [DataMember(Name = "rank")]
        public string Rank { get; set; }
        [DataMember(Name = "kingdom")]
        public string Kingdom { get; set; }
        [DataMember(Name = "phylum")]
        public string Phylum { get; set; }
        [DataMember(Name = "class")]
        public string Class { get; set; }
        [DataMember(Name = "order")]
        public string Order { get; set; }
        [DataMember(Name = "family")]
        public string Family { get; set; }
        [DataMember(Name = "genus")]
        public string Genus { get; set; }
    }
}