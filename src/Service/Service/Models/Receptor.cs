using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Receptor
    {
        public int Id { get; set; }
        public string NucleoSequence { get; set; }
        public string AminoSequence { get; set; }
        public ICollection<VSegment> VSegments { get; set; }
        public ICollection<DSegment> DSegments { get; set; }
        public ICollection<JSegment> JSegments { get; set; }
        public int LastVNucleoPos { get; set; }
        public int FirstDNucleoPos { get; set; }
        public int LastDNucleoPos { get; set; }
        public int FirstJNucleoPos { get; set; }
        public int VDInsertions { get; set; }
        public int DJInsertions { get; set; }
        public int TotalInsertions { get; set; }
        public ICollection<PersonalReceptor> PersonalReceptors { get; set; }

        public Receptor()
        {
            VSegments = new List<VSegment>();
            DSegments = new List<DSegment>();
            JSegments = new List<JSegment>();
            PersonalReceptors = new List<PersonalReceptor>();
        }

    }
}