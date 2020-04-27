using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Microsoft.VisualBasic.CompilerServices;

namespace przykladoweKolokwium1.Models.Reguests
{
    public class AddAnimalsReguest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AnimalType { get; set; }
        [Required]
        public string DateOfAdmission { get; set; }
        [Required]
        public int IdOwner { get; set; }
        public List<Procedure_Animal> ProcedureAnimals { get; set; }
    }
}