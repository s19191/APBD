using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "client")]
    [Route("api/campaigns")]
    public class CampaignController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }
        private IAdvertDbService _service;
        
        public CampaignController(IConfiguration configuration, IAdvertDbService service)
        {
            Configuration = configuration;
            _service = service;
        }

        [HttpGet("aaa")]
        public IActionResult aaaa()
        {
            return Ok(_service.aaa());
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetCampaings()
        {
            var i = User.Identities.GetEnumerator().Current.Name;
            return Ok(i);
        }
    }
}