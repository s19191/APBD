using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Klient
    {
        public int IdKlient { get; set; }
        public string Imie { get; set; }
        public string Nazwisk { get; set; }
        
        public ICollection<Zamowienie> Zamowienie { get; set; }
    }
}