using System;
using System.Linq;
using System.Net;
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
                    var students = db.Student;
                    string finalStudents = "";
                    foreach (var s in students)
                    {
                        finalStudents += s.ToString() + "\n";
                    }
                    return Ok("dodany student\n" + finalStudents);
                }
                return BadRequest(400);
            }
            return BadRequest(400);
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            var db = new s19191Context();
            var IdStudy = db.Studies
                .Where(s => s.Name.Equals(request.Studies));
            if (!IdStudy.IsNullOrEmpty())
            {
                var IdEnrollment = db.Enrollment
                    .Where(e => e.IdStudy.Equals(IdStudy.First().IdStudy) && e.Semester.Equals(request.Semester));
                if (!IdEnrollment.IsNullOrEmpty())
                {
                    var tmp = db.Enrollment
                        .Where(e => e.IdStudy.Equals(IdStudy.First().IdStudy) && e.Semester.Equals(request.Semester + 1));
                    Enrollment EnrollmentInserting = null;
                    if (tmp.IsNullOrEmpty())
                    {
                        int maxIdEnrollment = db.Enrollment.Max(e => e.IdEnrollment);
                        EnrollmentInserting = new Enrollment
                        {
                            IdEnrollment = maxIdEnrollment + 1,
                            Semester = request.Semester + 1,
                            IdStudy = IdStudy.First().IdStudy,
                            StartDate = DateTime.Today
                        };
                        db.Enrollment.Add(EnrollmentInserting);
                    }
                    else
                    {
                        EnrollmentInserting = tmp.First();
                    }
                    int IdEnrollmentInserting = EnrollmentInserting.IdEnrollment;
                    var update = db.Student
                        .Where(s => s.IdEnrollment.Equals(IdEnrollment.First().IdEnrollment));
                    foreach (var student in update)
                    {
                        student.IdEnrollment = IdEnrollmentInserting;
                    }
                    db.SaveChanges();
                    return Ok("Wszystkich studentów z semestru " + request.Semester + "oraz z kierunku " + request.Studies + " zostali przeniesieni na następny semestr");
                }
                return BadRequest(HttpStatusCode.NotFound);
            }
            return BadRequest(HttpStatusCode.NotFound);
        }
    }
}