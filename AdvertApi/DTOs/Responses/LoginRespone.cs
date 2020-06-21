namespace AdvertApi.DTOs.Responses
{
    public class LoginRespone
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public LoginRespone(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return "Klient zalogowany prawidłowo";
        }
    }
}