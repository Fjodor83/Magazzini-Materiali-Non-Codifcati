using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagazziniMaterialiAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase 
    {
        // GET: api/Admin
        [HttpGet]

        public IActionResult Get()
        {
            return Ok("Hello Admin");
        }
    }
}
