using System;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Models
{
    public class Studnet
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string nazwaStudniow { get; set; }
        public int Semester { get; set; }
    }
}