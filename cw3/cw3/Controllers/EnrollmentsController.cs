using System;
using System.Data.SqlClient;
using cw3.DTOs.Reguests;
using cw3.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            _service.EnrollStudent(request);
            return Ok(201);
        }

        [HttpPost("{promotions}")]
        public IActionResult EnrollPromotions(EnrollmentPromotionsRequest request)
        {


            return Ok(200);
        }
    }
}