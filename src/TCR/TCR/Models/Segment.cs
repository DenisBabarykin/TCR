using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TCR.Models
{
    public abstract class Segment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
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
    public class VSegment : Segment { }
    public class DSegment : Segment { }
    public class JSegment : Segment { }
}