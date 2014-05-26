using System;
using System.Collections.Generic;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models
{
    public class Spotter : IEntity<int>
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public virtual IEnumerable<Sighting> Sightings { get; set; }
    }
}