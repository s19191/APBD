using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class Zamowienie_WyrobCukierniczyEfConfiguration : IEntityTypeConfiguration<Zamowienie_WyrobCukierniczy>
    {
        public void Configure(EntityTypeBuilder<Zamowienie_WyrobCukierniczy> builder)
        {
            builder
                .HasKey(e => e.IdWyrobuCukierniczego);
            builder
                .HasKey(e => e.IdZamowienia);
            builder
                .Property(e => e.IdZamowienia)
                .HasAnnotation("ForeignKey", "IdZamowienia");
            builder
                .Property(e => e.IdWyrobuCukierniczego)
                .HasAnnotation("ForeignKey", "IdWyrobuCukierniczego");
            builder
                .Property(e => e.Ilosc)
                .IsRequired();
            builder
                .Property(e => e.Uwagi)
                .HasMaxLength(300);
            var klients = new List<Zamowienie_WyrobCukierniczy>();
            klients.Add(new Zamowienie_WyrobCukierniczy
            {
                IdZamowienia = 1,
                IdWyrobuCukierniczego = 1,
                Ilosc = 2,
                Uwagi = "aaaaaa"
            });
            builder.HasData(klients);
        }
    }
}