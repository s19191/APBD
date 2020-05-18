using cw11.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Models
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Doctor> Doctor { get; set; }

        public DbSet<Prescription> Prescription { get; set; }

        public DbSet<Patient> Patient { get; set; }

        public DbSet<Medicament> Medicament { get; set; }

        public DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }

        public HospitalDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MedicamentEfConfiguration());

            modelBuilder.ApplyConfiguration(new DoctorEfConfiguration());

            modelBuilder.ApplyConfiguration(new PatientEfConfiguration());

            modelBuilder.ApplyConfiguration(new PrescriptionEfConfiguration());

            modelBuilder.ApplyConfiguration(new Prescription_MedicamentEfConfiguration());
        }
    }
}
