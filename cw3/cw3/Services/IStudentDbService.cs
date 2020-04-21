using cw3.DTOs.Reguests;
using cw3.DTOs.Responses;

namespace cw3.Services
{
    public interface IStudentDbService
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        EnrollmentPromotionsResponse PromoteStudnet(EnrollmentPromotionsRequest request);
        bool CheckIndexNumber(string index);
        LoginRespone Loggining(LoginRequest request);
        refreshTokenResponse checkRefreshToken(string refreshToken);
        void saveRefreshToken(string index, string refreshToken);
    }
}