using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using cw3.DTOs.Reguests;
using cw3.DTOs.Responses;
using cw3.Models;
using cw3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace cw3.Controllers
{
    [ApiController]
    [Authorize(Roles = "employee")]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19191;Integrated Security=True";
        public IConfiguration Configuration { get; set; }
        
        private IStudentDbService _service;
        
        public StudentsController(IConfiguration configuration, IStudentDbService service)
        {
            Configuration = configuration;
            _service = service;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            LoginRespone response = _service.Loggining(request);
            if (response != null)
            {
                if (response.exsists)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier,response.index));
                    claims.Add(new Claim(ClaimTypes.Name,response.name));
                    for (int i = 0; i < response.roles.Count; i++)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, response.roles[i]));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken
                    (
                        issuer: "Gakko",
                        audience: "Students",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: creds
                    );
                    var refreshToken = Guid.NewGuid();
                    _service.saveRefreshToken(request.index, refreshToken.ToString());
                    return Ok(new
                    {
                        sccesstoken = new JwtSecurityTokenHandler().WriteToken(token),
                        refreshToken = refreshToken
                    });
                }
                return BadRequest(401 + " " + response);
            }
            return BadRequest(401);
        }

        [AllowAnonymous]
        [HttpPost("refresh/{request}")]
        public IActionResult RefreshToken(string request)
        {
            refreshTokenResponse response = _service.checkRefreshToken(request);
            if (response != null)
            {
                if (response.refreshTokenchecker)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier,response.LoginRespone.index));
                    claims.Add(new Claim(ClaimTypes.Name,response.LoginRespone.name));
                    for (int i = 0; i < response.LoginRespone.roles.Capacity; i++)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, response.LoginRespone.roles[i]));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken
                    (
                        issuer: "Gakko",
                        audience: "Students",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: creds
                    );
                    var refreshToken = Guid.NewGuid();
                    _service.saveRefreshToken(response.LoginRespone.index, refreshToken.ToString());
                    return Ok(new
                    {
                        sccesstoken = new JwtSecurityTokenHandler().WriteToken(token),
                        refreshToken = refreshToken
                    });
                }
                return BadRequest(401 + " " + response);
            }
            return BadRequest(401);
        }
        
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
                    studnet.IndexNumber = dr["IndexNumber"].ToString();
                    studnet.FirstName = dr["FirstName"].ToString();
                    studnet.LastName = dr["LastName"].ToString();
                    studnet.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());
                    studnet.Studies = dr["Name"].ToString();
                    studnet.Semester = (int) dr["Semester"];
                    studnets.Add(studnet);
                }
            }
            return Ok(studnets);
        }

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
                    result += "Semestr: " + dr["Semester"] + ", StartDate: " + dr["StartDate"];
                }
            }
            return Ok(result);
        }
    }
}