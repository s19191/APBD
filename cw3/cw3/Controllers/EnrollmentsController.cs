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
                    try
                    {
                    com.CommandText = "select IdStudies from Studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("Studia nie istnieją!");
                    }

                    int idStudies = (int) dr["IdStudies"];
                    com.CommandText = "select Semester, IdStudy from Enrollment where max(IdEnrollemnt)=IdEnrollment";
                    // 46:15



                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    return BadRequest("Nieznany błąd, operacja wycofana!");
                }
            }

            return Ok(200);
        }
    }
}