using cw11.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Services
{
    public class EfHospitalDbService : IHospitalDbService
    {
        private readonly HospitalDbContext _context;

        public EfHospitalDbService(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _context.Doctor.ToList();
        }

        public string AddDoctor(Doctor doctor)
        { 
            try
            {
                _context.Doctor.Add(doctor);
                _context.SaveChanges();
                return "Lekarz został dodany do bazy danych";
            }
            catch(Exception e)
            {
                return "Lekarz nie został dodany do bazy danych, przez następujący czynnik:\n" + e.Message;
            }
        }

        public string UpdateDoctor(Doctor doctor)
        {
            try
            {
                _context.Attach(doctor);
                _context.Entry(doctor).State = EntityState.Modified;
                _context.SaveChanges();
                return "Dane lekarza zostały zaktualizowane";
            }
            catch (Exception e)
            {
                return "Dane lekarza nie zostały zaktualizowane, przez następujący czynnik:\n" + e.Message;
            }
        }

        public string DelteDoctor(int IdDoctor)
        {
            try
            {
                var doctor = _context.Doctor
                    .FirstOrDefault(d => d.IdDoctor == IdDoctor);
                if (doctor != null)
                {
                    _context.Doctor.Remove(doctor);
                    _context.SaveChanges();
                    return "Lekarz został usunięty z bazy danych";
                }
                return "Nie ma lekarza o podanym indexie";
            }
            catch (Exception e)
            {
                return "Lekarz nie został usunięty z bazy danych, przez następujący czynnik:\n" + e.Message;
            }
        }
    }
}
