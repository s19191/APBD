namespace AdvertApi.DTOs.Responses
{
    public class refreshTokenResponse
    {
        public LoginRespone LoginRespone;
        public bool refreshTokenchecker;

        public refreshTokenResponse(LoginRespone loginRespone, bool refreshTokenchecker)
        {
            this.LoginRespone = loginRespone;
            this.refreshTokenchecker = refreshTokenchecker;
        }
        
        public override string ToString()
        {
            if (refreshTokenchecker)
            {
                return "Prawidłowy token " + LoginRespone;
            }
            return "Nieprawidłowy refreshToken";
        }
    }
}