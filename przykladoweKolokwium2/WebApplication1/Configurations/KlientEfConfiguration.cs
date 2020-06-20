using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class KlientEfConfiguration : IEntityTypeConfiguration<Klient>
    {
        public void Configure(EntityTypeBuilder<Klient> builder)
        {
            builder
                .HasKey(e => e.IdKlient);
            builder
                .Property(e => e.IdKlient)
                .ValueGeneratedOnAdd();
            builder
                .Property(e => e.Imie)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .Property(e => e.Nazwisk)
                .HasMaxLength(50)
                .IsRequired();
            var klients = new List<Klient>();
            klients.Add(new Klient
            {
                IdKlient = 1,
                Imie = "Ala",
                Nazwisk = "Kota"
            });
            builder.HasData(klients);
        }
    }
}