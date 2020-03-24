using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using cw3.DAL;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19191;Integrated Security=True";

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        // public string GetStudent(string orderBy)
        // {
        //     return $"Kowalski, Malewski, Andrzejewski srortowanie={orderBy}";
        // }

        // [HttpGet]
        // public IActionResult GetStudents(string orderBy)
        // {
        //     return Ok(_dbService.GetStudents());
        // }
        
        [HttpGet]
        public IActionResult GetStudnet()
        {
            List<Studnet> studnets = new List<Studnet>();
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Student inner join Enrollment on Student.IdEnrollment=Enrollment.IdEnrollment inner join Studies on Enrollment.IdStudy=Studies.IdStudy";
                
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Studnet studnet = new Studnet();
                    studnet.FirstName = dr["FirstName"].ToString();
                    studnet.LastName = dr["LastName"].ToString();
                    studnet.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());
                    studnet.nazwaStudniow = dr["Name"].ToString();
                    studnet.Semester = (int) dr["Semester"];
                    studnets.Add(studnet);
                }
            }
            return Ok(studnets);
        }
        
        // [HttpPost]
        // public IActionResult CreateStudent(Studnet studnet)
        // {
        //     studnet.IndexNumber = $"s{new Random().Next(1, 2000)}";
        //     return Ok(studnet);
        // }

        [HttpPut("{id}")]
        public IActionResult putStudent(int id)
        {
            if(id == 2)
            {
                return Ok("Aktualizacja dokończona");
            }
            return NotFound("Nie znaleziono studenta");
        }

        [HttpDelete("{id}")]
        public IActionResult deleteStudent(int id)
        {
            if(id == 1)
            {
                return Ok("Usuwanie ukończone");
            }
            return NotFound("Nie znaleziono studenta");
        }
    }
}