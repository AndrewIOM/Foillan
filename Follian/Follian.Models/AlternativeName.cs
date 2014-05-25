using System;

namespace Foillan.Models
{
    public enum Language
    {
        English = 1,
        Manx = 2
    }

    public class AlternativeName
    {
        public int ID { get; set; }
        public Language Language { get; set; }
        public String Name { get; set; }
    }
}