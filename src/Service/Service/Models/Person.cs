using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public ICollection<PersonalReceptor> PersonalReceptors { get; set; }

        public Person()
        {
            PersonalReceptors = new List<PersonalReceptor>();
        }
    }
}