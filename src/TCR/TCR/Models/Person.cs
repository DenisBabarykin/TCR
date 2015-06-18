using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TCR.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }
        public ICollection<PersonalReceptor> PersonalReceptors { get; set; }

        public Person()
        {
            PersonalReceptors = new List<PersonalReceptor>();
        }
    }
}