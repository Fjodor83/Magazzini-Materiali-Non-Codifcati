using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace MagazziniMaterialiAPI.Models
{
    public class RegisterModel
    {


        public required string Email { get; set; }
        public required string Password { get; set; }


    }
}
