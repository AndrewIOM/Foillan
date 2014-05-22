using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follian.Models
{
    public class FollianContext : DbContext
    {
        public FollianContext()
            : base("FollianContext")
        {

        }

        public DbSet<Taxon> Taxa { get; set; }
        public DbSet<Sighting> Sightings { get; set; }
        public DbSet<Spotter> Spotter { get; set; }
    }
}
