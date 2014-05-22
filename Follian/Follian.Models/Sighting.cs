﻿using Follian.Models.Geography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follian.Models
{
    public class Sighting
    {
        public int ID { get; set; }
        public int TaxonID { get; set; }
        public int SpotterID { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public virtual Taxon Taxon { get; set; }
        public virtual Spotter SpottedBy { get; set; }
    }
}