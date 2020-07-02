using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Exceptions;
using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "client")]
    [Route("api/campaigns")]
    public class CampaignsController : ControllerBase
    {
        private IAdvertDbService _service;
        
        public CampaignsController(IAdvertDbService service)
        {
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