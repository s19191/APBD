using System;
using System.ComponentModel.DataAnnotations;

namespace cw3.DTOs.Reguests
{
    public class EnrollStudentRequest
    {
        [Required]
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }
        [Required(ErrorMessage = "Muisz podać imie")]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Musisz podać naziwsko")]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Musisz podać datę urodzenia")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Musisz podać nazwę kierunku studiów")]
        public string Studies { get; set; }
    }
}