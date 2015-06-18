using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TCR.Models
{
    public class PersonalReceptor
    {
        public int Id { get; set; }

        [Required]
        public int ReadCount { get; set; }

        [Required]
        public double Percentage { get; set; }

        [Required]
        public int ReceptorId { get; set; }
        public Receptor Receptor { get; set; }

        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}