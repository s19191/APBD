using System.Linq;
using cw3.DTOs.Reguests;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public IActionResult UpdateStudnet(Student request)
        {
            var db = new s19191Context();
            db.Attach(request);
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();
            return Ok("Student zmodyfikowany");
        }
        
        [HttpPost("delete/{Index}")]
        public IActionResult DeleteStudnet(string Index)
        {
            var db = new s19191Context();
            var s = new Student
            {
                IndexNumber = Index
            };
            db.Attach(s);
            db.Student.Remove(s);
            db.SaveChanges();
            return Ok("Studnet usunięty z bazy");
        }
    }
}