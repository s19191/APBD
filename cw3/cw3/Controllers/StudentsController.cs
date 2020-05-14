using System;
using System.Linq;
using Castle.Core.Internal;
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

        [HttpPost("update")]
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

        [HttpPost("enroll")]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            String message;
            var db = new s19191Context();
            var studies = db.Studies.Where(s => s.Name.Equals(request.Studies));
            if (!studies.IsNullOrEmpty())
            {
                var MaxStartDate = db.Enrollment
                    .Max(e => e.StartDate);
                var enrollment = db.Enrollment
                    .Where(e => e.StartDate.Equals(MaxStartDate))
                    .Where(e => e.IdStudy.Equals(studies.First().IdStudy));
                int maxIdEnrollment;
                if (enrollment.IsNullOrEmpty())
                {
                    maxIdEnrollment = db.Enrollment.Max(e => e.IdEnrollment);
                    maxIdEnrollment++;
                    db.Enrollment.Add(new Enrollment
                    {
                        IdEnrollment = maxIdEnrollment,
                        Semester = 1,
                        IdStudy = studies.First().IdStudy,
                        StartDate = DateTime.Now
                    });
                }
                else
                {
                    maxIdEnrollment = enrollment.First().IdEnrollment;
                }
                var idStudnet = db.Student
                    .Where(s => s.IndexNumber.Equals(request.IndexNumber));
                if (!idStudnet.IsNullOrEmpty())
                {
                    var student = new Student
                    {
                        IndexNumber = request.IndexNumber,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        BirthDate = request.BirthDate,
                        IdEnrollment = maxIdEnrollment,
                        Salt = request.Salt,
                        RefreshToken = request.RefreshToken,
                        Password = request.Password
                    };
                    db.Student.Add(student);
                    db.SaveChanges();
                    message = "dodany student";
                }
                else
                {
                    message = "400";
                }
            }
            else
            {
                message = "400";
            }
            return Ok(message);
        }

        [HttpPost("promote")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            return Ok();
        }
    }
}