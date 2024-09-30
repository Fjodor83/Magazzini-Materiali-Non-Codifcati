using System.ComponentModel.DataAnnotations;

namespace MagazziniMaterialiAPI.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public required string Email { get; set; } = string.Empty;
        [StringLength(50, MinimumLength = 6)]
        public required string Password { get; set; } = string.Empty;
    }
}
