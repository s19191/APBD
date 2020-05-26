using System;

namespace cw3.Models
{
    public partial class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdEnrollment { get; set; }
        public string Salt { get; set; }
        public string RefreshToken { get; set; }
        public string Password { get; set; }

        public virtual Enrollment IdEnrollmentNavigation { get; set; }

        public override string ToString()
        {
            return "IndexNumber: " + IndexNumber + ", FirstName: " + FirstName + ", LastName: " + LastName +
                   ", BirthDate: " + BirthDate + ", IdEnrollment: " + IdEnrollment + ", Salt: " + Salt +
                   ", RefreshToken: " + RefreshToken + ", Password: " + Password + ".";
        }
    }
}
