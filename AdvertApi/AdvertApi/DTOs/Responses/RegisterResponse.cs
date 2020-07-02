using AdvertApi.Models;

namespace AdvertApi.DTOs.Responses
{
    public class RegisterResponse
    {
        public int IdClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public TokensReponseViewModel TokensReponse { get; set; }

        public void addTokens(string accesToken, string refreshToken)
        {
            TokensReponse.AccessToken = accesToken;
            TokensReponse.RefreshToken = refreshToken;
        }
    }
}