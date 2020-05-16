using System.Collections.Generic;
using cw3.Models;

namespace cw3.DTOs.Responses
{
    public class EnrollStudentResponse
    {
        public List<Student> allStudents { get; set; }
        public Student student { get; set; }
        public string studies { get; set; }

        public EnrollStudentResponse(List<Student> allStudents, Student student, string studies)
        {
            this.allStudents = allStudents;
            this.student = student;
            this.studies = studies;
        }
        
        public override string ToString()
        {
            string tmp = "";
            foreach (var student in allStudents)
            {
                tmp += student + "\n";
            }
            return "Student: " + student + " zosytał dodany do bazy danych na studia: " + studies + ".\nLista studentów znajujących się aktualnie w bazie danych:\n" + tmp;
        }
    }
}