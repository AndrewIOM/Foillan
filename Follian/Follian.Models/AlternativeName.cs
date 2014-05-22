using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follian.Models
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