﻿using System.ComponentModel.DataAnnotations;

namespace Foillan.DataTransferObjects
{
    public class Taxonomy
    {
        public string Kingdom { get; set; }
        public string Phylum { get; set; }
        public string Class { get; set; }
        public string Order { get; set; }
        public string Family { get; set; }
        public string Genus { get; set; }
        public string Species { get; set; }
        public string SubSpecies { get; set; }
    }
}