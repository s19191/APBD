using System.Linq;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetStudnet()
        {
            var db = new s19191Context();
            var students = db.Student.ToList();
            return Ok(students);
        }
        [HttpPost("delete/{Index}")]
        public IActionResult DeleteStudnet(string Index)
        {
            
            return Ok(200);
        }
    }
}