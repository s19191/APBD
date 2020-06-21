using System.Collections.Generic;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;

namespace AdvertApi.Services
{
    public interface IAdvertDbService
    {
        public Client checkRefreshToken(string refreshToken);
        public LoginRespone Loggining(LoginRequest request);
        public RegisterResponse Registration(RegisterRequest request);
        public void saveRefreshToken(string Login, string refreshToken);
        public IEnumerable<GetCampaignsResponse> GetCampaigns();
        public AddCampaignResponse AddCampaign(AddCampaignRequest request);
    }
}