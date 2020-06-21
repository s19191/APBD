using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "client")]
    [Route("api/campaigns")]
    public class CampaignController : ControllerBase
    {
        private IConfiguration Configuration { get; }
        private IAdvertDbService _service;
        
        public CampaignController(IConfiguration configuration, IAdvertDbService service)
        {
            Configuration = configuration;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetCampaings()
        {
            return Ok(_service.GetCampaigns());
        }
    }
}