using System.ComponentModel.DataAnnotations;

namespace MagazziniMaterialiAPI.Models.Entity
{
    public class Classificazione
    {
        [Key]
        public string CodiceClassificazione { get; set; }
        public string NomeClassificazione { get; set; }

        public ICollection<Materiale> Materiali { get; set; }
    }
}
