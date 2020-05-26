using cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Configurations
{
    public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            {
                var doctors = new List<Doctor>();
                doctors.Add(new Doctor
                {
                    IdDoctor = 1,
                    FirstName = "Adam",
                    LastName = "Kowalski",
                    Email = "adamkowal@wp.pl"
                });
                doctors.Add(new Doctor
                {
                    IdDoctor = 2,
                    FirstName = "Jan",
                    LastName = "Malinowski",
                    Email = "malinka@wp.pl"
                });

                builder.HasData(doctors);
            }
        }
    }
}
