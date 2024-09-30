using System.ComponentModel.DataAnnotations;

namespace MagazziniMaterialiCLient.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "L'email è obbligatoria")]
        [EmailAddress(ErrorMessage = "Formato email non valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria")]
        public string Password { get; set; }

        public LoginModel()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
