﻿using Microsoft.EntityFrameworkCore;

namespace AdvertApi.Models
{
    public partial class s19191Context : DbContext
    {
        public s19191Context()
        {
        }

        public s19191Context(DbContextOptions<s19191Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<Client> Client { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s19191;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.HasKey(e => e.IdAdvertisement)
                    .HasName("Banner_pk");

                entity.Property(e => e.IdAdvertisement).ValueGeneratedNever();

                entity.Property(e => e.Area).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.Banner)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Banner_Campaign");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.HasKey(e => e.IdBuilding)
                    .HasName("Building_pk");

                entity.Property(e => e.IdBuilding).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Height).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasKey(e => e.IdCampaign)
                    .HasName("Campaign_pk");

                entity.Property(e => e.IdCampaign).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.PricePerSquareMeter).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.FromBuildingNavigation)
                    .WithMany(p => p.CampaignFromBuildingNavigation)
                    .HasForeignKey(d => d.FromBuilding)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Campaign_Building2");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Campaign_Client");

                entity.HasOne(d => d.ToBuildingNavigation)
                    .WithMany(p => p.CampaignToBuildingNavigation)
                    .HasForeignKey(d => d.ToBuilding)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Campaign_Building1");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("Client_pk");

                entity.Property(e => e.IdClient).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RefreshToken)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
