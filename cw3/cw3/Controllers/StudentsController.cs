using System;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        public string GetStudent(string orderBy)
        {
            return $"Kowalski, Malewski, Andrzejewski srortowanie={orderBy}";
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

        [HttpDelete]
        public IActionResult DeleteStudnet(Studnet studnet)
        {
            return Ok("200 Usuwanie ukończone");
        }

        [HttpPut]
        public IActionResult PutStudnet(Studnet studnet)
        {
            return Ok("200 Aktualizacja dokończona");
        }
    }
}