using System;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    [XmlType(TypeName ="student")]
    public class Student
    {
        [XmlAttribute]
        public string indexNumber { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string birthDate { get; set; }
        public string email { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }
        public Studies studies { get; set; }

        public Student(string indexNumber, string fname, string lname, DateTime birthDate, string email, string mothersName, string fathersName, Studies studies)
        {
            this.indexNumber = "s"+indexNumber;
            this.fname = fname;
            this.lname = lname;
            this.birthDate = birthDate.ToString("d");
            this.email = email;
            this.mothersName = mothersName;
            this.fathersName = fathersName;
            this.studies = studies;
        }
        public Student()
        {
            indexNumber = null;
            fname = null;
            lname = null;
            birthDate = DateTime.Today.ToString("d");
            email = null;
            mothersName = null;
            fathersName = null;
            studies = null;
        }
    }
}