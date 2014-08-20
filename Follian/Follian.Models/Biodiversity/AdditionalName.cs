using System;
using Foillan.Models.DataAccess.Abstract;

namespace Foillan.Models.Biodiversity
{
    public enum Language
    {
        English,
        Manx
    }

    public class AdditionalName : IEntity<int>
    {
        public int Id { get; set; }
        public int SpeciesDetailsId { get; set; }
        public Language Language { get; set; }
        public String Name { get; set; }

        public virtual AdditionalDetails SpeciesDetails { get; set; }
    }
}