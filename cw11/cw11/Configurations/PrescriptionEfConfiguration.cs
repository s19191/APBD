using cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Configurations
{
    public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            {
                var prescriptions = new List<Prescription>();
                prescriptions.Add(new Prescription
                {
                    IdPrescription = 1,
                    Date = DateTime.Today,
                    DueDate = DateTime.Today,
                    IdPatient = 1,
                    IdDoctor = 2
                });
                prescriptions.Add(new Prescription
                {
                    IdPrescription = 2,
                    Date = DateTime.Today,
                    DueDate = DateTime.Today,
                    IdPatient = 2,
                    IdDoctor = 2
                });
                prescriptions.Add(new Prescription
                {
                    IdPrescription = 3,
                    Date = DateTime.Today,
                    DueDate = DateTime.Today,
                    IdPatient = 2,
                    IdDoctor = 2
                });

                builder.HasData(prescriptions);
            }
        }
    }
}