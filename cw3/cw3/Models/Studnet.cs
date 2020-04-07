using System;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Models
{
    public class Studnet
    {
        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
    }
}