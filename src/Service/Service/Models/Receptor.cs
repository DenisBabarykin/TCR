using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Receptor
    {
        public int ReceptorId { get; set; }
        public int ReadCount { get; set; }
        public double Percentage { get; set; }
        public string NucleoSequence { get; set; }
        public string AminoSequence { get; set; }
        public string VSegments { get; set; }
        public string JSegments { get; set; }
        public string DSegments { get; set; }
        public int LastVNucleoPos { get; set; }
        public int FirstDNucleoPos { get; set; }
        public int LastDNucleoPos { get; set; }
        public int FirstJNucleoPos { get; set; }
        public int VDInsertions { get; set; }
        public int DJInsertions { get; set; }
        public int TotalInsertions { get; set; }

        public ICollection<Person> People { get; set; }

        public Receptor()
        {
            People = new List<Person>();
        }

    }
}