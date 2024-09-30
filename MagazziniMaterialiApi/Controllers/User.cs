using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagazziniMaterialiAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello User");
        }
    }
}
