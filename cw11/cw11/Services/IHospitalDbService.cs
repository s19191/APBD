using cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Services
{
    public interface IHospitalDbService
    {
        public IEnumerable<Doctor> GetDoctors();

        public string AddDoctor(Doctor doctor);

        public string UpdateDoctor(Doctor doctor);

        public string DelteDoctor(int IdDoctor);
    }
}
