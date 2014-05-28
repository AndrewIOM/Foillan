using System;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models
{
    public enum Language
    {
        English = 1,
        Manx = 2
    }

    public class AlternativeName : IEntity<int>
    {
        public int Id { get; set; }
        public int SpeciesDetailsId { get; set; }
        public Language Language { get; set; }
        public String Name { get; set; }

        public virtual SpeciesDetails SpeciesDetails { get; set; }
    }
}