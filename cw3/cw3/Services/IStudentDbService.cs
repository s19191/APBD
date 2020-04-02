using cw3.DTOs.Reguests;

namespace cw3.Services
{
    public interface IStudentDbService
    {
        void EnrollStudent(EnrollStudentRequest request);
        void PromoteStudnet(EnrollmentPromotionsRequest request);
    }
}