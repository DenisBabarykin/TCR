using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Segment
    {
        public int SegmentId { get; set; }
        public string Alleles { get; set; }
        public int CDR3_Position { get; set; }
        public string FullNucleoSequence { get; set; }
        public string NucleoSequence { get; set; }
        public string NucleoSequenceP { get; set; }
        public ICollection<Receptor> Receptors { get; set; }

        public Segment()
        {
            Receptors = new List<Receptor>();
        }
    }
}