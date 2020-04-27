using System.ComponentModel.DataAnnotations;
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
        public IntegerType IdOwner { get; set; }
    }
}