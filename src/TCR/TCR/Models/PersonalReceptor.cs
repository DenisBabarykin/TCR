using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCR.Models
{
    public class PersonalReceptor
    {
        public int Id { get; set; }
        public int ReadCount { get; set; }
        public double Percentage { get; set; }

        public int ReceptorId { get; set; }
        public Receptor Receptor { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}