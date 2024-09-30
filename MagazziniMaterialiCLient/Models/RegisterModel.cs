using MagazziniMaterialiCLient.Models.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MagazziniMaterialiCLient.Models
{
    public class RegisterModel
    {


        public required string Email { get; set; }
        public required string Password { get; set; }


    }
}
