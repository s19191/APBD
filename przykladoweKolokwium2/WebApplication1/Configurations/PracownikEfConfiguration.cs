using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class PracownikEfConfiguration : IEntityTypeConfiguration<Pracownik>
    {
        public void Configure(EntityTypeBuilder<Pracownik> builder)
        {
            builder
                .HasKey(e => e.IdPracown);
            builder
                .Property(e => e.IdPracown)
                .ValueGeneratedOnAdd();
            builder
                .Property(e => e.Imie)
                .IsRequired();
            builder
                .Property(e => e.Nazwisko)
                .IsRequired();
            var klients = new List<Pracownik>();
            klients.Add(new Pracownik
            {
                IdPracown = 1,
                Imie = "Ola",
                Nazwisko = "Koti"
            });
            builder.HasData(klients);
        }
    }
}