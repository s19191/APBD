using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Authorize(Roles = "employee")]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
    
    }
}