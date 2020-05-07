using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using przykladoweKolokwium1.Models.Reguests;
using przykladoweKolokwium1.Models.Responses;
using przykladoweKolokwium1.Services;

namespace przykladoweKolokwium1.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        
        private IAnimalsDbService _service;
        
        public AnimalsController(IConfiguration configuration, IAnimalsDbService service)
        {
            Configuration = configuration;
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAnimals(string sortBy)
        {
            List<GetAnimalsResponse> response = _service.GetAnimals(sortBy);
            if (response == null)
            {
                return BadRequest(400);
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddAnimal(AddAnimalsReguest reguest)
        {
            bool IfSucces = _service.AddAnimal(reguest);
            if (IfSucces)
            {
                return Ok();
            }
            return BadRequest(400);
        }
    }
}