using System;
using System.Linq;
using System.Net;
using cw3.DTOs.Reguests;
using cw3.DTOs.Responses;
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
            var db = new s19191Context();
            var studies = db.Studies
                .FirstOrDefault(s => s.Name.Equals(request.Studies));
            if (studies != null)
            {
                var maxStartDate = db.Enrollment
                    .Max(e => e.StartDate);
                var enrollment = db.Enrollment
                    .Where(e => e.StartDate.Equals(maxStartDate))
                    .FirstOrDefault(e => e.IdStudy.Equals(studies.IdStudy));
                int maxIdEnrollment;
                if (enrollment == null)
                {
                    maxIdEnrollment = db.Enrollment.Max(e => e.IdEnrollment);
                    maxIdEnrollment++;
                    db.Enrollment.Add(new Enrollment
                    {
                        IdEnrollment = maxIdEnrollment,
                        Semester = 1,
                        IdStudy = studies.IdStudy,
                        StartDate = DateTime.Now
                    });
                }
                else
                {
                    maxIdEnrollment = enrollment.IdEnrollment;
                }
                var idStudnet = db.Student
                    .FirstOrDefault(s => s.IndexNumber.Equals(request.IndexNumber));
                if (idStudnet == null)
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
                    var allStudents = db.Student.ToList();
                    EnrollStudentResponse response = new EnrollStudentResponse(allStudents, student, request.Studies);
                    return Ok(response.ToString());
                }
                return BadRequest(400 + ", student o podanym indexie już istnieje");
            }
            return BadRequest(400 + ", podane Studia nie istnieją");
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            var db = new s19191Context();
            var IdStudy = db.Studies
                .FirstOrDefault(s => s.Name.Equals(request.Studies));
            if (IdStudy != null)
            {
                var IdEnrollment = db.Enrollment
                    .FirstOrDefault(e => e.IdStudy.Equals(IdStudy.IdStudy) && e.Semester.Equals(request.Semester));
                if (IdEnrollment != null)
                {
                    var EnrollmentInserting = db.Enrollment
                        .FirstOrDefault(e => e.IdStudy.Equals(IdStudy.IdStudy) && e.Semester.Equals(request.Semester + 1));
                    if (EnrollmentInserting == null)
                    {
                        int maxIdEnrollment = db.Enrollment.Max(e => e.IdEnrollment);
                        EnrollmentInserting = new Enrollment
                        {
                            IdEnrollment = maxIdEnrollment + 1,
                            Semester = request.Semester + 1,
                            IdStudy = IdStudy.IdStudy,
                            StartDate = DateTime.Today
                        };
                        db.Enrollment.Add(EnrollmentInserting);
                    }
                    int IdEnrollmentInserting = EnrollmentInserting.IdEnrollment;
                    var update = db.Student
                        .Where(s => s.IdEnrollment.Equals(IdEnrollment.IdEnrollment));
                    foreach (var student in update)
                    {
                        student.IdEnrollment = IdEnrollmentInserting;
                    }
                    db.SaveChanges();
                    PromoteStudentResponse response = new PromoteStudentResponse(request.Studies, request.Semester);
                    return Ok(response.ToString());
                }
                return BadRequest(HttpStatusCode.NotFound);
            }
            return BadRequest(HttpStatusCode.NotFound + ", podane Studia nie istnieją");
        }
    }
}