using System;
using System.ComponentModel.DataAnnotations;
using Foillan.Models.DataAccess.Abstract;

namespace Foillan.Models.Geography
{
    public class Location : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public override string ToString()
        {
            return String.Format("{0}, {1}", Latitude, Longitude);
        }
    }
}
