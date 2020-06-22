using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Exceptions;
using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "client")]
    [Route("api/campaigns")]
    public class CampaignsController : ControllerBase
    {
        private IConfiguration Configuration { get; }
        private IAdvertDbService _service;
        
        public CampaignsController(IConfiguration configuration, IAdvertDbService service)
        {
            Configuration = configuration;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetCampaings()
        {
            return Ok(_service.GetCampaigns());
        }

        [HttpPost("add")]
        public IActionResult AddCampaign(AddCampaignRequest request)
        {
            try
            {
                AddCampaignResponse response = _service.AddCampaign(request);
                return Ok(response);
            }
            catch (NoSuchClientException e)
            {
                return NotFound(e.Message);
            }
            catch (NotOnTheSameStreetOrCityException e)
            {
                return BadRequest(e.Message);
            }
            catch (NotEnoughtBuildingsInDatabaseException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}