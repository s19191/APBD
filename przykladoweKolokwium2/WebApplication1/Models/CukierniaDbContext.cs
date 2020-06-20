using Microsoft.EntityFrameworkCore;
using WebApplication1.Configurations;

namespace WebApplication1.Models
{
    public class CukierniaDbContext : DbContext
    {
        public DbSet<Klient> Klient { get; set; }

        public DbSet<Pracownik> Pracownik { get; set; }

        public DbSet<Zamowienie> Zamowienie { get; set; }

        public DbSet<WyrobCukierniczy> WyrobCukierniczy { get; set; }

        public DbSet<Zamowienie_WyrobCukierniczy> Zamowienie_WyrobCukierniczy { get; set; }

        public CukierniaDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new KlientEfConfiguration());

            modelBuilder.ApplyConfiguration(new PracownikEfConfiguration());

            modelBuilder.ApplyConfiguration(new ZamowienieEfConfiguration());

            modelBuilder.ApplyConfiguration(new WyrobCukierniczyEfConfiguration());

            modelBuilder.ApplyConfiguration(new Zamowienie_WyrobCukierniczyEfConfiguration());

            modelBuilder.Entity<Klient>()
                .HasMany(c => c.Zamowienie)
                .WithOne(c => c.Klient);
            modelBuilder.Entity<Pracownik>()
                .HasMany(c => c.Zamowienie)
                .WithOne(c => c.Pracownik);
            modelBuilder.Entity<Zamowienie>()
                .HasMany(c => c.ZamowienieWyrobCukierniczie)
                .WithOne(c => c.Zamowienie);
            modelBuilder.Entity<WyrobCukierniczy>()
                .HasMany(c => c.ZamowienieWyrobCukierniczie)
                .WithOne(c => c.WyrobCukierniczy);
        }
    }
}