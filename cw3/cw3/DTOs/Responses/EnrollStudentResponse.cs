namespace cw3.DTOs.Responses
{
    public class EnrollStudentResponse
    {
        private int Semester { get; set; }

        public EnrollStudentResponse(int Semester)
        {
            this.Semester = Semester;
        }

        public override string ToString()
        {
            return "Semestr: " + Semester;
        }
    }
}