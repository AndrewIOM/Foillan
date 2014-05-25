using System;
using System.Collections.Generic;
using Foillan.Models.DataAccessLayer.Concrete;

namespace Foillan.Models
{
    public class Spotter : Entity<int>
    {
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public virtual IEnumerable<Sighting> Sightings { get; set; }
    }
}