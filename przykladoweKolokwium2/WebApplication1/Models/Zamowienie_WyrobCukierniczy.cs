using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Zamowienie_WyrobCukierniczy
    {
        public int IdZamowienia { get; set; }
        public virtual Zamowienie Zamowienie { get; set; }
        public int IdWyrobuCukierniczego { get; set; }
        public virtual WyrobCukierniczy WyrobCukierniczy { get; set; }
        public int Ilosc { get; set; }
        public string Uwagi { get; set; }
    }
}