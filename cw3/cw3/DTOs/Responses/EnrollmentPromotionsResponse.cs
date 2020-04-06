namespace cw3.DTOs.Responses
{
    public class EnrollmentPromotionsResponse
    {
        public int Semester { get; set; }

        public EnrollmentPromotionsResponse(int Semester)
        {
            this.Semester = Semester;
        }

        public override string ToString()
        {
            return "Semestr na który studenci zostali przepisani: " + Semester;
        }
    }
}