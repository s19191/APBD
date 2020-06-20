using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class ZamowienieEfConfiguration : IEntityTypeConfiguration<Zamowienie>
    {
        public void Configure(EntityTypeBuilder<Zamowienie> builder)
        {
            builder
                .HasKey(e => e.IdZamowienia);
            builder
                .Property(e => e.IdZamowienia)
                .ValueGeneratedOnAdd();
            builder
                .Property(e => e.DataPrzyjecia)
                .IsRequired();
            builder
                .Property(e => e.Uwagi)
                .HasMaxLength(300);
            builder
                .Property(e => e.IdKlient)
                .HasAnnotation("ForeignKey", "IdKlient");
            builder
                .Property(e => e.IdPracownik)
                .HasAnnotation("ForeignKey", "IdPracown");
            var klients = new List<Zamowienie>();
            klients.Add(new Zamowienie
            {
                IdZamowienia = 1,
                DataPrzyjecia = DateTime.Now,
                DataRealizacji = DateTime.Now,
                Uwagi = "aalaaaa",
                IdKlient = 1,
                IdPracownik = 1
            });
            builder.HasData(klients);
        }
    }
}