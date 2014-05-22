using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follian.Models
{
    public class Spotter
    {
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public virtual IEnumerable<Sighting> Sightings { get; set; }
    }
}