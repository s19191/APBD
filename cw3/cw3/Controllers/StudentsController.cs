using System;
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

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        public string GetStudent(string orderBy)
        {
            return $"Kowalski, Malewski, Andrzejewski srortowanie={orderBy}";
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }
        
        [HttpGet("{id}")]
        public IActionResult GetStudnet(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }
            return NotFound("Nie znaleziono studenta");
        }
        
        [HttpPost]
        public IActionResult CreateStudent(Studnet studnet)
        {
            studnet.IndexNumber = $"s{new Random().Next(1, 2000)}";
            return Ok(studnet);
        }

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