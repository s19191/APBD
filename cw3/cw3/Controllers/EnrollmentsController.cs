using System;
using System.Data.SqlClient;
using cw3.DTOs.Reguests;
using cw3.DTOs.Responses;
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
            EnrollStudentResponse response = _service.EnrollStudent(request);
            if (request != null)
            {
                return Ok(201 + response.ToString());
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{promotions}")]
        public IActionResult EnrollPromotions(EnrollmentPromotionsRequest request)
        {
            EnrollmentPromotionsResponse response = _service.PromoteStudnet(request);
            if (response != null)
            {
                return Ok(201 + response.ToString());
            }
            else
            {
                return BadRequest();
            }
        }
    }
}