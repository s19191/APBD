using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class WyrobCukierniczyEfConfiguration : IEntityTypeConfiguration<WyrobCukierniczy>
    {
        public void Configure(EntityTypeBuilder<WyrobCukierniczy> builder)
        {
            builder
                .HasKey(e => e.IdWyrobuCukierniczego);
            builder
                .Property(e => e.IdWyrobuCukierniczego)
                .ValueGeneratedOnAdd();
            builder
                .Property(e => e.Nazwa)
                .HasMaxLength(200)
                .IsRequired();
            builder
                .Property(e => e.Typ)
                .HasMaxLength(40)
                .IsRequired();
            var klients = new List<WyrobCukierniczy>();
            klients.Add(new WyrobCukierniczy
            {
                IdWyrobuCukierniczego = 1,
                Nazwa = "ciastko",
                CenaZaSzt = 6,
                Typ = "kot"
            });
            builder.HasData(klients);
        }
    }
}