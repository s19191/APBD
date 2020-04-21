using System.Collections.Generic;

namespace cw3.DTOs.Responses
{
    public class LoginRespone
    {
        public string index { get; set; }
        public string name { get; set; }
        public List<string> roles { get; set; }
        public bool exsists { get; set; }
        public bool passwordCorrect { get; set; }
        
        public LoginRespone(string index, string name, List<string> roles, bool exsists, bool passwordCorrect)
        {
            this.index = index;
            this.name = name;
            this.roles = roles;
            this.exsists = exsists;
            this.passwordCorrect = passwordCorrect;
        }
        
        public override string ToString()
        {
            if (exsists)
            {
                if (passwordCorrect)
                {
                    return "Użytkownik: " + name + ", o indexie: " + index + ", o rolach: " + roles;
                }
                return "Nie prawidłowe hasło! Ty oszuście okropny!";
            }
            return "Użytkownik nie istnieje";
        }
    }
}