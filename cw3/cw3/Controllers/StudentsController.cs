using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19191;Integrated Security=True";
        
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
        
        //przykład SQL Injection:
        //https://localhost:44395/api/students/1 = 1 or 2
        //po dodaniu parametru na stronie nie wyświetliło się nic

        [HttpGet("{IndexNumber}")]
         public IActionResult GetSemester(string IndexNumber)
         {
             string result = "";
             using (SqlConnection con = new SqlConnection(ConString))
             using (SqlCommand com = new SqlCommand())
             {
                 com.Connection = con;
                 com.CommandText = "select * from Enrollment inner join Student on Student.IdEnrollment=Enrollment.IdEnrollment where @IndexNumber=IndexNumber";
                 com.Parameters.AddWithValue("IndexNumber", IndexNumber);
                 
                 con.Open();
                 SqlDataReader dr = com.ExecuteReader();
                 while (dr.Read())
                 {
                     result += "Semestr: " + dr["Semester"] + ", StartDate: " + dr["StartDate"].ToString();
                 }
             }
             return Ok(result);
         }
    }
}