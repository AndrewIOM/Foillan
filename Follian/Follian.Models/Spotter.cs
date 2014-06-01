using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Foillan.Models.DataAccessLayer.Abstract;

namespace Foillan.Models
{
    public class Spotter : IEntity<int>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public virtual IEnumerable<Sighting> Sightings { get; set; }
    }
}