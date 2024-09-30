using System.ComponentModel.DataAnnotations;

namespace MagazziniMaterialiCLient.Models.Entity
{
    public class Classificazione
    {
        [Key]
        public string CodiceClassificazione { get; set; }
        public string NomeClassificazione { get; set; }
    }
}
