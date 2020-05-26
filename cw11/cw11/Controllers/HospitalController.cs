using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw11.Models;
using cw11.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw11.Controllers
{
    [Route("api/hospital")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalDbService _context;

        public HospitalController(IHospitalDbService context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(_context.GetDoctors());
        }

        [HttpPut]
        public IActionResult AddDoctor(Doctor doctor)
        {
            string message = _context.AddDoctor(doctor);
            
            if(message.Equals("Lekarz został dodany do bazy danych"))
            {
                return Ok(message);
            }
            return BadRequest(400 + message);
        }

        [HttpPost]
        public IActionResult UpdateDoctor(Doctor doctor)
        {
            string message = _context.UpdateDoctor(doctor);

            if (message.Equals("Dane lekarza zostały zaktualizowane"))
            {
                return Ok(message);
            }
            return BadRequest(400 + message);
        }

        [HttpPost("{IdDoctor}")]
        public IActionResult DeleteDoctor(string IdDoctor)
        {
            string message = _context.DelteDoctor(Int32.Parse(IdDoctor));

            if (message.Equals("Lekarz został usunięty z bazy danych"))
            {
                return Ok(message);
            }
            return BadRequest(400 + message);
        }
    }
}
