using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class WyrobCukierniczy
    {
        public int IdWyrobuCukierniczego { get; set; }
        public string Nazwa { get; set; }
        public float CenaZaSzt { get; set; }
        public string Typ { get; set; }
        public ICollection<Zamowienie_WyrobCukierniczy> ZamowienieWyrobCukierniczie { get; set; }
    }
}