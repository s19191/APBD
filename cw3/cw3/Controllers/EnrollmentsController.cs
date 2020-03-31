using System;
using System.Data.SqlClient;
using cw3.DTOs.Reguests;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19191;Integrated Security=True";
        
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                    try
                    {
                    com.CommandText = "select IdStudy from Studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);
                    SqlDataReader dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("Studia nie istnieją!");
                    }
                    int IdStudy = (int) dr["IdStudy"];
                    dr.Close();
                    com.CommandText = "select a.IdEnrollment from Enrollment a where a.StartDate = (select max(StartDate) from Enrollment where Semester=1 and IdStudy=" + IdStudy + ")";
                    int maxIdEnrollment;
                    dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        com.CommandText = "select max(IdEnrollment) from Enrollment";
                        dr = com.ExecuteReader();
                        if (!dr.Read())
                        {
                            tran.Rollback(); 
                            return BadRequest("Błąd przy tworzeniu Enrollment!");
                        }
                        maxIdEnrollment = (int) dr[0] + 1;
                        dr.Close();
                        DateTime today = DateTime.Today;
                        com.CommandText = "insert into Enrollment(IdEnrollment, Semester, IdStudy, StartDate) values(" + maxIdEnrollment + ", 1," + IdStudy + ", @today)";
                        com.Parameters.AddWithValue("today", today);
                        com.ExecuteNonQuery();
                    }
                    else
                    {
                        maxIdEnrollment = (int) dr[0];
                        dr.Close();
                    }
                    com.CommandText = "select 1 from Student where IndexNumber=@IndexNumber";
                    com.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("Student o podanym indexie już istnieje!");
                    }
                    dr.Close();
                    com.CommandText = "insert into Student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values(@IndexNumber, @FirstName, @LastName, @BirthDate, " + maxIdEnrollment + ")";
                    com.Parameters.AddWithValue("FirstName", request.FirstName);
                    com.Parameters.AddWithValue("LastName", request.LastName);
                    com.Parameters.AddWithValue("BirthDate", request.BirthDate);
                    com.ExecuteNonQuery();
                    tran.Commit();
                    return Ok(201);
                    }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    return BadRequest("Nieznany błąd, operacja wycofana!");
                }
            }
        }
    }
}