using cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Configurations
{
    public class Prescription_MedicamentEfConfiguration : IEntityTypeConfiguration<Prescription_Medicament>
    {
        public void Configure(EntityTypeBuilder<Prescription_Medicament> builder)
        {
            {
                builder
                .HasKey(e => e.IdMedicament);

                builder
                .HasKey(e => e.IdPrescription);

                builder
                    .Property(e => e.Dose)
                    .IsRequired();

                builder
                    .Property(e => e.Details)
                    .HasMaxLength(100)
                    .IsRequired();

                var prescription_medicaments = new List<Prescription_Medicament>();
                prescription_medicaments.Add(new Prescription_Medicament
                {
                    IdMedicament = 1,
                    IdPrescription = 1,
                    Dose = 145,
                    Details = "nie stosować w ciąży"
                });
                prescription_medicaments.Add(new Prescription_Medicament
                {
                    IdMedicament = 2,
                    IdPrescription = 2,
                    Dose = 1000,
                    Details = "nie łączyć z alkoholem"
                });
                prescription_medicaments.Add(new Prescription_Medicament
                {
                    IdMedicament = 1,
                    IdPrescription = 3,
                    Dose = 2,
                    Details = "brać mądrze"
                });

                builder.HasData(prescription_medicaments);
            }
        }
    }
}
