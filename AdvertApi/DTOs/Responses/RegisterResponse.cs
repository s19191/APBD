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
        public string accesstoken { get; set; }
        public string refreshToken { get; set; }

        public RegisterResponse(Client client, string accesstoken)
        {
            client.IdClient = IdClient;
            client.FirstName = FirstName;
            client.LastName = LastName;
            client.Email = Email;
            client.Phone = Phone;
            client.Login = Login;
            client.Password = Password;
            client.RefreshToken = refreshToken;
            this.accesstoken = accesstoken;
        }
    }
}