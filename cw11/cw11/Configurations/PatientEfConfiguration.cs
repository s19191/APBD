using cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Configurations
{
    public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            {
                var patients = new List<Patient>();
                patients.Add(new Patient
                {
                    IdPatient = 1,
                    FirstName = "Ala",
                    LastName = "Kowalewska",
                    BirthDate = DateTime.Today
                });
                patients.Add(new Patient
                {
                    IdPatient = 2,
                    FirstName = "Ola",
                    LastName = "Malanowska",
                    BirthDate = DateTime.Today
                });

                builder.HasData(patients);
            }
        }
    }
}