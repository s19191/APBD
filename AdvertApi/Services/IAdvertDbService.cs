using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;

namespace AdvertApi.Services
{
    public interface IAdvertDbService
    {
        public Client checkRefreshToken(string refreshToken);
        public LoginRespone Loggining(LoginRequest request);
        public Client Registration(RegisterRequest request);
        public void saveRefreshToken(string Login, string refreshToken);
    }
}