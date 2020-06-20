using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Pracownik
    {
        public int IdPracown { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public ICollection<Zamowienie> Zamowienie { get; set; }
    }
}