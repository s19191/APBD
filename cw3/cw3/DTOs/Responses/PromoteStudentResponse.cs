namespace cw3.DTOs.Responses
{
    public class PromoteStudentResponse
    {
        public string Studies { get; set; }
        public int Semester { get; set; }

        public PromoteStudentResponse(string studies, int semester)
        {
            Studies = studies;
            Semester = semester;
        }

        public override string ToString()
        {
            return "Wszyscy studenci z semestru " + Semester + " oraz z kierunku " + Studies +
                   " zostali przeniesieni na następny semestr";
        }
    }
}